using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.GraphQLClients;
using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Pages.SampleThinhLcs
{
    public partial class SampleThinhLCDetail
    {
        [Parameter] public int Id { get; set; }

        private SampleThinhLcGraphQLResponse? sample;
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadSample();
        }

        private async Task LoadSample()
        {
            isLoading = true;
            try
            {
                sample = await GraphQLConsumer.GetSampleThinhLCById(Id);

                if (sample == null)
                {
                    await JSRuntime.InvokeVoidAsync("alert", "Kh�ng t�m th?y sample!");
                    NavigateBack();
                }
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", $"L?i khi t?i d? li?u: {ex.Message}");
                NavigateBack();
            }
            finally
            {
                isLoading = false;
            }
        }

        private void NavigateToEdit()
        {
            Navigation.NavigateTo($"/SampleThinhLCForm/{Id}");
        }

        private void NavigateBack()
        {
            Navigation.NavigateTo("/SampleThinhLCList");
        }

        private async Task ConfirmDelete()
        {
            bool confirmed = await JSRuntime.InvokeAsync<bool>("confirm", "B?n c� ch?c ch?n mu?n x�a sample n�y?");
            if (confirmed)
            {
                await DeleteSample();
            }
        }

        private async Task DeleteSample()
        {
            try
            {
                bool success = await GraphQLConsumer.DeleteSampleThinhLC(Id);
                if (success)
                {
                    await JSRuntime.InvokeVoidAsync("alert", "X�a th�nh c�ng!");
                    NavigateBack();
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "X�a th?t b?i!");
                }
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", $"L?i: {ex.Message}");
            }
        }
    }
}