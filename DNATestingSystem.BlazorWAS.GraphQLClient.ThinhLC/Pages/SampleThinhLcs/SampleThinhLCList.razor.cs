using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.GraphQLClients;
using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models;
using Microsoft.JSInterop;

namespace DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Pages.SampleThinhLcs
{
    public partial class SampleThinhLCList
    {
        private List<SampleThinhLcGraphQLResponse> samples = new List<SampleThinhLcGraphQLResponse>();
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadSamples();
        }
        private async Task LoadSamples()
        {
            isLoading = true;
            try
            {
                samples = await GraphQLConsumer.GetSampleThinhLCs() ?? new List<SampleThinhLcGraphQLResponse>();
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
                    await LoadSamples(); // Reload the list
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