using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.GraphQLClients;
using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models;
using Microsoft.JSInterop;

namespace DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Pages.SampleThinhLcs
{
    public partial class SampleThinhLCList
    {
        private List<SampleThinhLcGraphQLResponse> samples = new();
        private bool isLoading = true;
        private int currentPage = 1;
        private int totalPages = 1;

        protected override async Task OnInitializedAsync()
        {
            // Kiểm tra đăng nhập (ví dụ: kiểm tra localStorage)
            var user = await JSRuntime.InvokeAsync<string>("localStorage.getItem", "currentUser");
            if (string.IsNullOrEmpty(user))
            {
                Navigation.NavigateTo("/");
                return;
            }
            await LoadSamples(currentPage);
        }

        private async Task LoadSamples(int page)
        {
            isLoading = true;
            try
            {
                var result = await GraphQLConsumer.GetSampleThinhLCPaged(page);
                Console.WriteLine("Paged result: " + System.Text.Json.JsonSerializer.Serialize(result));
                samples = result?.Items ?? new List<SampleThinhLcGraphQLResponse>();
                currentPage = result?.CurrentPage ?? 1;
                totalPages = result?.TotalPages ?? 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading samples: {ex.Message}");
                samples = new List<SampleThinhLcGraphQLResponse>();
            }
            finally
            {
                isLoading = false;
            }
        }

        private async Task PreviousPage()
        {
            if (currentPage > 1)
                await LoadSamples(currentPage - 1);
        }

        private async Task NextPage()
        {
            if (currentPage < totalPages)
                await LoadSamples(currentPage + 1);
        }

        private async Task GoToPage(int page)
        {
            if (page != currentPage)
                await LoadSamples(page);
        }

        private void NavigateToAdd()
        {
            Navigation.NavigateTo("/SampleThinhLCForm");
        }

        private void NavigateToEdit(int? id)
        {
            if (id.HasValue)
            {
                Navigation.NavigateTo($"/SampleThinhLCForm/{id.Value}");
            }
        }

        private void NavigateToDetail(int? id)
        {
            if (id.HasValue)
            {
                Navigation.NavigateTo($"/SampleThinhLCDetail/{id.Value}");
            }
        }

        private async Task ConfirmDelete(int? id)
        {
            if (id.HasValue)
            {
                bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Bạn có chắc chắn muốn xóa sample này?");
                if (confirmed)
                {
                    await DeleteSample(id.Value);
                }
            }
        }

        private async Task DeleteSample(int id)
        {
            try
            {
                bool success = await GraphQLConsumer.DeleteSampleThinhLC(id);
                if (success)
                {
                    await JSRuntime.InvokeVoidAsync("alert", "Xóa thành công!");
                    await LoadSamples(currentPage); // Reload the current page
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "Xóa thất bại!");
                }
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", $"Lỗi: {ex.Message}");
            }
        }
    }
}