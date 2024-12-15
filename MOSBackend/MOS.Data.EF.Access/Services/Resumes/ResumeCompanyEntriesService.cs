﻿using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.Mappings.Resumes;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Resumes;

namespace MOS.Data.EF.Access.Services.Resumes;

public class ResumeCompanyEntriesService : IResumeCompanyEntriesService
{
    private readonly IResumeCompanyEntriesRepository resumeCompanyEntriesRepository;
    
    public ResumeCompanyEntriesService(IResumeCompanyEntriesRepository resumeCompanyEntriesRepository)
    {
        this.resumeCompanyEntriesRepository = resumeCompanyEntriesRepository;
    }

    public async Task<OperationResult<ResumeCompanyEntryResponseDto>> CreateResumeCompanyEntryAsync(ResumeCompanyEntryCreateRequestDto resumeCompanyEntryRequest)
    {
        var resumeCompanyEntry = new ResumeCompanyEntry()
        {
            ResumeId = resumeCompanyEntryRequest.ResumeId,
            Company = resumeCompanyEntryRequest.Company,
            Description = resumeCompanyEntryRequest.Description,
            WebSiteUrl = resumeCompanyEntryRequest.WebSiteUrl,
        };

        var createdEntity = await resumeCompanyEntriesRepository.CreateAsync(resumeCompanyEntry);

        return createdEntity.ToDto();
    }

    public async Task<OperationResult<ResumeCompanyEntryResponseDto>> UpdateResumeCompanyEntryAsync(ResumeCompanyEntryUpdateRequestDto resumeCompanyEntryRequest)
    {
        var resumeCompanyEntry = await resumeCompanyEntriesRepository.GetByIdAsync(resumeCompanyEntryRequest.Id);
        if (resumeCompanyEntry == null)
        {
            return OperationError.NotFound();
        }

        resumeCompanyEntry.Company = resumeCompanyEntryRequest.Company;
        resumeCompanyEntry.Description = resumeCompanyEntryRequest.Description;
        resumeCompanyEntry.WebSiteUrl = resumeCompanyEntryRequest.WebSiteUrl;

        await resumeCompanyEntriesRepository.UpdateAsync(resumeCompanyEntry);
        
        return resumeCompanyEntry.ToDto();
    }

    public void Dispose()
    {
        resumeCompanyEntriesRepository.Dispose();
    }
}