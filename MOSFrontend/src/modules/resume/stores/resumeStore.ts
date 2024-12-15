import { defineStore } from "pinia";
import { computed, ref } from "vue";
import { TResume } from "@/shared/types";
import ResumesServiceInstance from "@/shared/services/ResumesService.ts";
import { TCreateResumeRequest, TUpdateResumeRequest } from "@/shared/services/base";

export const useResumeStore = defineStore("resume", () => {
  const resumesService = ResumesServiceInstance;

  const currentResumeLocal = ref<TResume | null>(null);

  const currentResume = computed(() => currentResumeLocal.value);

  async function getResume(resumeId: number) {
    try {
      const resume = await resumesService.getResume(resumeId);
      currentResumeLocal.value = resume;
      return resume;
    }
    catch (error) {
      throw error;
    }
  }

  async function createResume(params: TCreateResumeRequest): Promise<TResume> {
    try {
      const resume = await resumesService.createResume(params);
      currentResumeLocal.value = resume;
      return resume;
    }
    catch (error) {
      throw error;
    }
  }

  async function updateResume(params: TUpdateResumeRequest): Promise<TResume> {
    try {
      const resume = await resumesService.updateResume(params);
      currentResumeLocal.value = resume;
      return resume;
    }
    catch (error) {
      throw error;
    }
  }

  return {
    currentResume,

    getResume,
    createResume,
    updateResume
  };
});
