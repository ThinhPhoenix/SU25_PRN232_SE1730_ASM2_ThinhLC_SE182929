using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.GraphQLClients;
using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models;
using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models.DTOs;
using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Pages.SampleThinhLcs
{
    public partial class SampleThinhLCForm
    {
        [Parameter] public int? Id { get; set; }

        private SampleThinhLcGraphQLResponse sample = new();
        private SampleThinhLcInputDto sampleDto = new();
        private bool isSubmitting = false;
        private bool isLoading = true;
        private bool IsEdit => Id.HasValue && Id.Value > 0;

        // Dropdown data
        private List<ProfileThinhLc> profiles = new List<ProfileThinhLc>();
        private List<SampleTypeThinhLc> sampleTypes = new List<SampleTypeThinhLc>();
        private List<AppointmentsTienDm> appointments = new List<AppointmentsTienDm>();

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;

            // Load dropdown data
            await LoadDropdownData();

            if (IsEdit)
            {
                await LoadSample();
            }
            else
            {
                // Initialize with default values for new sample
                sampleDto.CollectedAt = DateTime.Now;
                sampleDto.CreatedAt = DateTime.Now;
                sampleDto.IsProcessed = false;
                sampleDto.Count = 1;
            }

            isLoading = false;
        }
        private async Task LoadDropdownData()
        {
            try
            {
                // Load all dropdown data in parallel
                var profilesTask = GraphQLConsumer.GetProfiles();
                var sampleTypesTask = GraphQLConsumer.GetSampleTypes();
                var appointmentsTask = GraphQLConsumer.GetAppointments();

                await Task.WhenAll(profilesTask, sampleTypesTask, appointmentsTask);

                profiles = await profilesTask ?? new List<ProfileThinhLc>();
                sampleTypes = await sampleTypesTask ?? new List<SampleTypeThinhLc>();
                appointments = await appointmentsTask ?? new List<AppointmentsTienDm>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dropdown data: {ex.Message}");
                await JSRuntime.InvokeVoidAsync("alert", $"Lỗi khi tải dữ liệu dropdown: {ex.Message}");

                // Initialize empty lists on error
                profiles = new List<ProfileThinhLc>();
                sampleTypes = new List<SampleTypeThinhLc>();
                appointments = new List<AppointmentsTienDm>();
            }
        }

        private async Task LoadSample()
        {
            if (!Id.HasValue) return;

            try
            {
                var existingSample = await GraphQLConsumer.GetSampleThinhLCById(Id.Value);
                if (existingSample != null)
                {
                    sample = existingSample;
                    sampleDto = existingSample.ToInputDto();
                }
                else
                {
                    await JSRuntime.InvokeVoidAsync("alert", "Không tìm thấy sample!");
                    NavigateBack();
                }
            }
            catch (Exception ex)
            {
                await JSRuntime.InvokeVoidAsync("alert", $"Lỗi khi tải dữ liệu: {ex.Message}");
                NavigateBack();
            }
        }
        private async Task HandleValidSubmit()
        {
            isSubmitting = true; try
            {
                // Validate required fields
                if (!sampleDto.ProfileThinhLcid.HasValue || sampleDto.ProfileThinhLcid <= 0)
                {
                    await JSRuntime.InvokeVoidAsync("alert", "Vui lòng chọn Profile!");
                    return;
                }

                if (!sampleDto.SampleTypeThinhLcid.HasValue || sampleDto.SampleTypeThinhLcid <= 0)
                {
                    await JSRuntime.InvokeVoidAsync("alert", "Vui lòng chọn Sample Type!");
                    return;
                }

                // Convert DTO back to GraphQL response for API call
                var sampleForApi = sampleDto.ToGraphQLResponse();
                if (IsEdit)
                {
                    sampleForApi.SampleThinhLcid = sampleDto.SampleThinhLcid;
                }

                Console.WriteLine($"Submitting sample data: {System.Text.Json.JsonSerializer.Serialize(sampleDto)}");

                bool success;
                if (IsEdit)
                {
                    success = await GraphQLConsumer.UpdateSampleThinhLC(sampleForApi);
                    if (success)
                    {
                        await JSRuntime.InvokeVoidAsync("alert", "Cập nhật thành công!");
                        NavigateBack();
                    }
                    else
                    {
                        await JSRuntime.InvokeVoidAsync("alert", "Cập nhật thất bại! Vui lòng kiểm tra console để xem chi tiết lỗi.");
                    }
                }
                else
                {
                    success = await GraphQLConsumer.CreateSampleThinhLC(sampleForApi);
                    if (success)
                    {
                        await JSRuntime.InvokeVoidAsync("alert", "Thêm mới thành công!");
                        NavigateBack();
                    }
                    else
                    {
                        await JSRuntime.InvokeVoidAsync("alert", "Thêm mới thất bại! Vui lòng kiểm tra console để xem chi tiết lỗi.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in HandleValidSubmit: {ex}");
                await JSRuntime.InvokeVoidAsync("alert", $"Lỗi: {ex.Message}");
            }
            finally
            {
                isSubmitting = false;
            }
        }

        private void NavigateBack()
        {
            Navigation.NavigateTo("/SampleThinhLCList", true);
        }
    }
}