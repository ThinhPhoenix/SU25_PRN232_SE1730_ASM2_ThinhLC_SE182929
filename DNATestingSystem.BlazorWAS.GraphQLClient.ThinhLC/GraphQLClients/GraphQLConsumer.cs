using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models;
using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models.DTOs;
using DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.Models.Extensions;
using GraphQL.Client.Abstractions;
using System.Linq;

namespace DNATestingSystem.BlazorWAS.GraphQLClient.ThinhLC.GraphQLClients
{
    public class GraphQLConsumer
    {
        private readonly IGraphQLClient _graphQLClient;
        public GraphQLConsumer(IGraphQLClient graphQLClient)
            => _graphQLClient = graphQLClient;
        public async Task<List<SampleThinhLcGraphQLResponse>> GetSampleThinhLCs()
        {
            try
            {
                var query = @"
                query {
                    sampleThinhLCs {
                        appointmentsTienDmid
                        collectedAt
                        count
                        createdAt
                        deletedAt
                        isProcessed
                        notes
                        profileThinhLcid
                        sampleThinhLcid
                        sampleTypeThinhLcid
                        updatedAt
                    }
                }"; Console.WriteLine($"Sending GraphQL Query: {query}");

                var response = await _graphQLClient.SendQueryAsync<dynamic>(query);

                Console.WriteLine($"GraphQL Response received: {response != null}");

                if (response?.Data?.sampleThinhLCs != null)
                {
                    var samples = new List<SampleThinhLcGraphQLResponse>();

                    foreach (var item in response.Data.sampleThinhLCs)
                    {
                        var sample = new SampleThinhLcGraphQLResponse
                        {
                            SampleThinhLcid = (int?)item.sampleThinhLcid,
                            ProfileThinhLcid = (int?)item.profileThinhLcid,
                            SampleTypeThinhLcid = (int?)item.sampleTypeThinhLcid,
                            AppointmentsTienDmid = (int?)item.appointmentsTienDmid,
                            Notes = item.notes?.ToString(),
                            IsProcessed = (bool?)item.isProcessed,
                            Count = (int?)item.count,
                            CollectedAt = item.collectedAt != null ? DateTime.Parse(item.collectedAt.ToString()) : null,
                            CreatedAt = item.createdAt != null ? DateTime.Parse(item.createdAt.ToString()) : null,
                            UpdatedAt = item.updatedAt != null ? DateTime.Parse(item.updatedAt.ToString()) : null,
                            DeletedAt = item.deletedAt != null ? DateTime.Parse(item.deletedAt.ToString()) : null
                        };

                        samples.Add(sample);
                    }
                    return samples;
                }

                return new List<SampleThinhLcGraphQLResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching samples: {ex.Message}");
                return new List<SampleThinhLcGraphQLResponse>();
            }
        }
        public async Task<bool> DeleteSampleThinhLC(int id)
        {
            try
            {
                var mutation = @"
                mutation($id: Int!) {
                    deleteSampleThinhLCs(id: $id)
                }";

                var variables = new { id = id };

                Console.WriteLine($"Sending GraphQL Mutation: {mutation}");
                Console.WriteLine($"Variables: {System.Text.Json.JsonSerializer.Serialize(variables)}");

                var response = await _graphQLClient.SendMutationAsync<dynamic>(mutation, variables);

                Console.WriteLine($"GraphQL Delete Response received: {response != null}");
                Console.WriteLine($"Response Data: {response?.Data}");

                if (response?.Data?.deleteSampleThinhLCs != null)
                {
                    return (bool)response.Data.deleteSampleThinhLCs;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting sample: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> CreateSampleThinhLC(SampleThinhLcGraphQLResponse sample)
        {
            try
            {
                var mutation = @"
                mutation($input: SampleThinhLcInput!) {
                    createSampleThinhLCs(sampleThinhLC: $input)
                }";

                var dto = sample.ToInputDto();

                var variables = new
                {
                    input = new
                    {
                        profileThinhLcid = dto.ProfileThinhLcid,
                        sampleTypeThinhLcid = dto.SampleTypeThinhLcid,
                        appointmentsTienDmid = dto.AppointmentsTienDmid,
                        notes = dto.Notes,
                        isProcessed = dto.IsProcessed,
                        count = dto.Count,
                        collectedAt = dto.CollectedAt?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                    }
                };

                Console.WriteLine($"Sending GraphQL Create Mutation: {mutation}");
                Console.WriteLine($"Variables: {System.Text.Json.JsonSerializer.Serialize(variables)}");

                var response = await _graphQLClient.SendMutationAsync<dynamic>(mutation, variables);

                Console.WriteLine($"GraphQL Create Response received: {response != null}");
                Console.WriteLine($"Response Data: {response?.Data}");

                if (response?.Data?.createSampleThinhLCs != null)
                {
                    var result = (int)response.Data.createSampleThinhLCs;
                    return result > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating sample: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> UpdateSampleThinhLC(SampleThinhLcGraphQLResponse sample)
        {
            try
            {
                var mutation = @"
                mutation($input: SampleThinhLcInput!) {
                    updateSampleThinhLCs(sampleThinhLC: $input)
                }";

                var dto = sample.ToInputDto();
                dto.UpdatedAt = DateTime.Now;

                var variables = new
                {
                    input = new
                    {
                        sampleThinhLcid = dto.SampleThinhLcid,
                        profileThinhLcid = dto.ProfileThinhLcid,
                        sampleTypeThinhLcid = dto.SampleTypeThinhLcid,
                        appointmentsTienDmid = dto.AppointmentsTienDmid,
                        notes = dto.Notes,
                        isProcessed = dto.IsProcessed,
                        count = dto.Count,
                        collectedAt = dto.CollectedAt?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                        updatedAt = dto.UpdatedAt?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                    }
                };

                Console.WriteLine($"Sending GraphQL Update Mutation: {mutation}");
                Console.WriteLine($"Variables: {System.Text.Json.JsonSerializer.Serialize(variables)}");

                var response = await _graphQLClient.SendMutationAsync<dynamic>(mutation, variables);

                Console.WriteLine($"GraphQL Update Response received: {response != null}");
                Console.WriteLine($"Response Data: {response?.Data}");

                if (response?.Data?.updateSampleThinhLCs != null)
                {
                    var result = (int)response.Data.updateSampleThinhLCs;
                    return result > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating sample: {ex.Message}");
                return false;
            }
        }

        public async Task<SampleThinhLcGraphQLResponse?> GetSampleThinhLCById(int id)
        {
            try
            {
                var query = @"
                query($id: Int!) {
                    sampleThinhLCById(id: $id) {
                        appointmentsTienDmid
                        collectedAt
                        count
                        createdAt
                        deletedAt
                        isProcessed
                        notes
                        profileThinhLcid
                        sampleThinhLcid
                        sampleTypeThinhLcid
                        updatedAt
                    }
                }";

                var variables = new { id = id };

                Console.WriteLine($"Sending GraphQL Query: {query}");

                var response = await _graphQLClient.SendQueryAsync<dynamic>(query, variables);

                Console.WriteLine($"GraphQL Response received: {response != null}");

                if (response?.Data?.sampleThinhLCById != null)
                {
                    var item = response.Data.sampleThinhLCById;
                    var sample = new SampleThinhLcGraphQLResponse
                    {
                        SampleThinhLcid = (int?)item.sampleThinhLcid,
                        ProfileThinhLcid = (int?)item.profileThinhLcid,
                        SampleTypeThinhLcid = (int?)item.sampleTypeThinhLcid,
                        AppointmentsTienDmid = (int?)item.appointmentsTienDmid,
                        Notes = item.notes?.ToString(),
                        IsProcessed = (bool?)item.isProcessed,
                        Count = (int?)item.count,
                        CollectedAt = item.collectedAt != null ? DateTime.Parse(item.collectedAt.ToString()) : null,
                        CreatedAt = item.createdAt != null ? DateTime.Parse(item.createdAt.ToString()) : null,
                        UpdatedAt = item.updatedAt != null ? DateTime.Parse(item.updatedAt.ToString()) : null,
                        DeletedAt = item.deletedAt != null ? DateTime.Parse(item.deletedAt.ToString()) : null
                    };

                    return sample;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching sample by id: {ex.Message}");
                return null;
            }
        }

        // Methods for dropdown lists
        public async Task<List<ProfileThinhLc>> GetProfiles()
        {
            try
            {
                var query = @"
                query {
                    profileThinhLCs {
                        profileThinhLcid
                        fullName
                        nationalId
                        gender
                        dateOfBirth
                        isDeceased
                        notes
                    }
                }";

                Console.WriteLine($"Sending GraphQL Query: {query}");

                var response = await _graphQLClient.SendQueryAsync<dynamic>(query);

                Console.WriteLine($"GraphQL Response received: {response != null}");

                if (response?.Data?.profileThinhLCs != null)
                {
                    var profiles = new List<ProfileThinhLc>();

                    foreach (var item in response.Data.profileThinhLCs)
                    {
                        var profile = new ProfileThinhLc
                        {
                            ProfileThinhLcid = (int)item.profileThinhLcid,
                            FullName = item.fullName?.ToString() ?? "",
                            NationalId = item.nationalId?.ToString(),
                            Gender = item.gender?.ToString(),
                            IsDeceased = (bool?)item.isDeceased ?? false,
                            Notes = item.notes?.ToString()
                        };

                        profiles.Add(profile);
                    }
                    return profiles;
                }

                return new List<ProfileThinhLc>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching profiles: {ex.Message}");
                return new List<ProfileThinhLc>();
            }
        }

        public async Task<List<SampleTypeThinhLc>> GetSampleTypes()
        {
            try
            {
                var query = @"
                query {
                    sampleTypeThinhLCs {
                        sampleTypeThinhLcid
                        typeName
                        description
                        isActive
                    }
                }";

                Console.WriteLine($"Sending GraphQL Query: {query}");

                var response = await _graphQLClient.SendQueryAsync<dynamic>(query);

                Console.WriteLine($"GraphQL Response received: {response != null}");

                if (response?.Data?.sampleTypeThinhLCs != null)
                {
                    var sampleTypes = new List<SampleTypeThinhLc>();

                    foreach (var item in response.Data.sampleTypeThinhLCs)
                    {
                        var sampleType = new SampleTypeThinhLc
                        {
                            SampleTypeThinhLcid = (int?)item.sampleTypeThinhLcid,
                            TypeName = item.typeName?.ToString(),
                            Description = item.description?.ToString(),
                            IsActive = (bool?)item.isActive
                        };

                        sampleTypes.Add(sampleType);
                    }
                    return sampleTypes;
                }

                return new List<SampleTypeThinhLc>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching sample types: {ex.Message}");
                return new List<SampleTypeThinhLc>();
            }
        }

        public async Task<List<AppointmentsTienDm>> GetAppointments()
        {
            try
            {
                var query = @"
                query {
                    appointmentsTienDMs {
                        appointmentsTienDmid
                        appointmentDate
                        appointmentTime
                        contactPhone
                        samplingMethod
                        address
                        notes
                        totalAmount
                        isPaid
                    }
                }";

                Console.WriteLine($"Sending GraphQL Query: {query}");

                var response = await _graphQLClient.SendQueryAsync<dynamic>(query);

                Console.WriteLine($"GraphQL Response received: {response != null}");

                if (response?.Data?.appointmentsTienDMs != null)
                {
                    var appointments = new List<AppointmentsTienDm>();

                    foreach (var item in response.Data.appointmentsTienDMs)
                    {
                        var appointment = new AppointmentsTienDm
                        {
                            AppointmentsTienDmid = (int)item.appointmentsTienDmid,
                            ContactPhone = item.contactPhone?.ToString() ?? "",
                            SamplingMethod = item.samplingMethod?.ToString() ?? "",
                            Address = item.address?.ToString(),
                            Notes = item.notes?.ToString(),
                            TotalAmount = (decimal?)item.totalAmount ?? 0,
                            IsPaid = (bool?)item.isPaid
                        };

                        // Handle DateOnly parsing if the field exists
                        if (item.appointmentDate != null)
                        {
                            if (DateOnly.TryParse(item.appointmentDate.ToString(), out DateOnly appointmentDate))
                            {
                                appointment.AppointmentDate = appointmentDate;
                            }
                        }

                        appointments.Add(appointment);
                    }
                    return appointments;
                }

                return new List<AppointmentsTienDm>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching appointments: {ex.Message}");
                return new List<AppointmentsTienDm>();
            }
        }

        public async Task<List<SampleTypeThinhLc>> GetSampleTypeThinhLCs()
        {
            try
            {
                var query = @"
                query {
                    sampleTypeThinhLCs {
                        sampleTypeThinhLcid
                        typeName
                        description
                        isActive
                        count
                        createdAt
                        updatedAt
                        deletedAt
                    }
                }";
                var response = await _graphQLClient.SendQueryAsync<dynamic>(query);
                if (response?.Data?.sampleTypeThinhLCs != null)
                {
                    var list = new List<SampleTypeThinhLc>();
                    foreach (var item in response.Data.sampleTypeThinhLCs)
                    {
                        var obj = new SampleTypeThinhLc
                        {
                            SampleTypeThinhLcid = (int?)item.sampleTypeThinhLcid,
                            TypeName = item.typeName,
                            Description = item.description,
                            IsActive = (bool?)item.isActive,
                            Count = (int?)item.count,
                            CreatedAt = item.createdAt != null ? DateTime.Parse(item.createdAt.ToString()) : null,
                            UpdatedAt = item.updatedAt != null ? DateTime.Parse(item.updatedAt.ToString()) : null,
                            DeletedAt = item.deletedAt != null ? DateTime.Parse(item.deletedAt.ToString()) : null
                        };
                        list.Add(obj);
                    }
                    return list;
                }
                return new List<SampleTypeThinhLc>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching sample types: {ex.Message}");
                return new List<SampleTypeThinhLc>();
            }
        }

        public async Task<bool> DeleteSampleTypeThinhLC(int id)
        {
            try
            {
                var mutation = @"
                mutation($id: Int!) {
                    deleteSampleTypeThinhLCs(id: $id)
                }";
                var variables = new { id = id };
                var response = await _graphQLClient.SendMutationAsync<dynamic>(mutation, variables);
                if (response?.Data?.deleteSampleTypeThinhLCs != null)
                {
                    return (bool)response.Data.deleteSampleTypeThinhLCs;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting sample type: {ex.Message}");
                return false;
            }
        }

        public async Task<SampleTypeThinhLc?> GetSampleTypeThinhLCById(int id)
        {
            try
            {
                var query = @"
                query($id: Int!) {
                    sampleTypeThinhLCById(id: $id) {
                        sampleTypeThinhLcid
                        typeName
                        description
                        isActive
                        count
                        createdAt
                        updatedAt
                        deletedAt
                    }
                }";
                var variables = new { id = id };
                var response = await _graphQLClient.SendQueryAsync<dynamic>(query, variables);
                if (response?.Data?.sampleTypeThinhLCById != null)
                {
                    var item = response.Data.sampleTypeThinhLCById;
                    return new SampleTypeThinhLc
                    {
                        SampleTypeThinhLcid = (int?)item.sampleTypeThinhLcid,
                        TypeName = item.typeName,
                        Description = item.description,
                        IsActive = (bool?)item.isActive,
                        Count = (int?)item.count,
                        CreatedAt = item.createdAt != null ? DateTime.Parse(item.createdAt.ToString()) : null,
                        UpdatedAt = item.updatedAt != null ? DateTime.Parse(item.updatedAt.ToString()) : null,
                        DeletedAt = item.deletedAt != null ? DateTime.Parse(item.deletedAt.ToString()) : null
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching sample type by id: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateSampleTypeThinhLC(SampleTypeThinhLc sampleType)
        {
            try
            {
                var mutation = @"
                mutation($input: SampleTypeThinhLcInput!) {
                    createSampleTypeThinhLCs(sampleTypeThinhLC: $input)
                }";
                var variables = new
                {
                    input = new
                    {
                        typeName = sampleType.TypeName,
                        description = sampleType.Description,
                        isActive = sampleType.IsActive ?? false,
                        count = sampleType.Count,
                        createdAt = sampleType.CreatedAt?.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                    }
                };
                var response = await _graphQLClient.SendMutationAsync<dynamic>(mutation, variables);
                if (response?.Data?.createSampleTypeThinhLCs != null)
                {
                    var result = (int)response.Data.createSampleTypeThinhLCs;
                    return result > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating sample type: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateSampleTypeThinhLC(SampleTypeThinhLc sampleType)
        {
            try
            {
                var mutation = @"
                mutation($input: SampleTypeThinhLcInput!) {
                    updateSampleTypeThinhLCs(sampleTypeThinhLC: $input)
                }";
                var variables = new
                {
                    input = new
                    {
                        sampleTypeThinhLcid = sampleType.SampleTypeThinhLcid,
                        typeName = sampleType.TypeName,
                        description = sampleType.Description,
                        isActive = sampleType.IsActive ?? false,
                        count = sampleType.Count,
                        updatedAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
                    }
                };
                var response = await _graphQLClient.SendMutationAsync<dynamic>(mutation, variables);
                if (response?.Data?.updateSampleTypeThinhLCs != null)
                {
                    var result = (int)response.Data.updateSampleTypeThinhLCs;
                    return result > 0;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating sample type: {ex.Message}");
                return false;
            }
        }
    }
}
