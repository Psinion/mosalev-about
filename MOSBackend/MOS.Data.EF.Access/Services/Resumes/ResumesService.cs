﻿using Microsoft.EntityFrameworkCore;
using MOS.Application.Data.Repositories;
using MOS.Application.Data.Repositories.Resumes;
using MOS.Application.Data.Services.Resumes;
using MOS.Application.Data.Services.Users;
using MOS.Application.DTOs.Resumes.Requests;
using MOS.Application.DTOs.Resumes.Responses;
using MOS.Application.Mappings.Resumes;
using MOS.Application.OperationResults;
using MOS.Domain.Entities.Resumes;
using MOS.Domain.Entities.Users;

namespace MOS.Data.EF.Access.Services.Resumes;

public class ResumesService : IResumesService
{
    private readonly ICredentialsService credentialsService;
    private readonly IUnitOfWork unitOfWork;
    private readonly IResumesRepository resumesRepository;
    
    public ResumesService(ICredentialsService credentialsService, IUnitOfWork unitOfWork, IResumesRepository resumesRepository)
    {
        this.credentialsService = credentialsService;
        this.unitOfWork = unitOfWork;
        this.resumesRepository = resumesRepository;
    }

    public async Task<OperationResult<ResumeResponseDto>> GetResumeAsync(int resumeId)
    {
        var resume = await resumesRepository.GetByIdWithRelationsAsync(resumeId);

        if (resume == null)
        {
            return OperationError.NotFound("Not found");
        }

        return resume.ToDto();
    }

    public async Task<OperationResult<ResumeResponseDto>> CreateResumeAsync(ResumeCreateRequestDto resumeRequest)
    {
        var resume = new Resume()
        {
            Title = resumeRequest.Title,
            About = resumeRequest.About,
        };

        var createdEntity = await resumesRepository.CreateAsync(resume);
        await unitOfWork.SaveChangesAsync();

        return createdEntity.ToDto();
    }

    public async Task<OperationResult<ResumeResponseDto>> UpdateResumeAsync(ResumeUpdateRequestDto resumeRequest)
    {
        var resume = await resumesRepository.GetByIdAsync(resumeRequest.Id);
        if (resume == null)
        {
            return OperationError.NotFound();
        }

        resume.Title = resumeRequest.Title;
        resume.About = resumeRequest.About;

        await resumesRepository.UpdateAsync(resume);
        await unitOfWork.SaveChangesAsync();
        
        return resume.ToDto();
    }
    
    public async Task<OperationResult<bool>> HasPinnedResumeAsync()
    {
        return await resumesRepository.GetPinnedResumeAsync(credentialsService.CurrentLocale) != null;
    }

    public async Task<OperationResult<ResumeResponseDto>> GetPinnedResumeAsync()
    {
        var resume = await resumesRepository.GetPinnedResumeAsync(credentialsService.CurrentLocale);

        if (resume == null)
        {
            return OperationError.NotFound(); 
        }

        return resume.ToDto();
    }

    public async Task<OperationResult<bool>> PinResumeAsync(int resumeId, bool pinning)
    {
        if (!resumesRepository.ExistsAsync(resumeId).Result)
        {
            return OperationError.NotFound();
        }

        await resumesRepository.UnpinAllByLocaleAsync(credentialsService.CurrentLocale);
        await resumesRepository.PinResumeAsync(resumeId, pinning ? credentialsService.CurrentLocale : null);

        return true;
    }
    
    public async Task<OperationResult<List<ResumeResponseCompactDto>>> GetCompactResumesListAsync()
    {
        var resumes = await resumesRepository.GetAll()
            .Where(x => x.DeletedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .ThenByDescending(x => x.UpdatedAt)
            .Select(x => new Resume()
        {
            Id = x.Id,
            Title = x.Title,
            PinnedToLocale = x.PinnedToLocale,
            CreatedAt = x.CreatedAt,
            CreatedBy = x.CreatedBy,
            Creator = new User()
            {
                Id = x.Creator.Id,
                FirstName = x.Creator.FirstName,
                LastName = x.Creator.LastName,
                Patronymic = x.Creator.Patronymic,
                UserName = x.Creator.UserName,
            },
            UpdatedAt = x.UpdatedAt,
            UpdatedBy = x.UpdatedBy,
            Updater = x.Updater != null ? new User()
            {
                Id = x.Updater.Id,
                FirstName = x.Updater.FirstName,
                LastName = x.Updater.LastName,
                Patronymic = x.Updater.Patronymic,
                UserName = x.Updater.UserName,
            } : null,
        }).ToListAsync();

        return resumes.ToCompactDtoList();
    }

    public void Dispose()
    {
        credentialsService.Dispose();
        resumesRepository.Dispose();
    }
}