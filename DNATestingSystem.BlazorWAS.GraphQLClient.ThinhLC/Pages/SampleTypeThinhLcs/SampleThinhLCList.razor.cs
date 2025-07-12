using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.GraphQLClients;
using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Pages.SampleTypeThinhLcs
{
    public partial class SampleThinhLCList
    {
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] private GraphQLConsumer GraphQLConsumer { get; set; } = default!;

        private List<SampleTypeThinhLc> sampleTypes = new List<SampleTypeThinhLc>();
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadSampleTypes();
        }
        private async Task LoadSampleTypes()
        {
            isLoading = true;
            try
            {
                sampleTypes = await GraphQLConsumer.GetSampleTypeThinhLCs() ?? new List<SampleTypeThinhLc>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sample types: {ex.Message}");
                sampleTypes = new List<SampleTypeThinhLc>();
            }
            finally
            {
                isLoading = false;
            }
        }

        private void NavigateToAdd()
        {
            Navigation.NavigateTo("/SampleTypeThinhLCForm");
        }

        private void NavigateToEdit(int? id)
        {
            if (id.HasValue)
            {
                Navigation.NavigateTo($"/SampleTypeThinhLCForm/{id.Value}");
            }
        }

        private void NavigateToDetail(int? id)
        {
            if (id.HasValue)
            {
                Navigation.NavigateTo($"/SampleTypeThinhLCDetail/{id.Value}");
            }
        }

        private async Task ConfirmDelete(int? id)
        {
            if (id.HasValue)
            {
                bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "Bạn có chắc chắn muốn xóa sample type này?");
                if (confirmed)
                {
                    await DeleteSampleType(id.Value);
                }
            }
        }

        private async Task DeleteSampleType(int id)
        {
            try
            {
                var result = await GraphQLConsumer.DeleteSampleTypeThinhLC(id);
                if (result)
                {
                    await LoadSampleTypes();
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "Xóa thất bại!");
                }
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", $"Lỗi khi xóa: {ex.Message}");
            }
        }
    }
}
