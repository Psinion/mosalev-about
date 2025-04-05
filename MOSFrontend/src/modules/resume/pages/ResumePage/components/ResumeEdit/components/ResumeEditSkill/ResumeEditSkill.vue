<template>
  <article class="resume-edit-skills">
    <PsiForm
      v-model:auto-submit-timer-stop="autoSubmitStop"
      class="form"
      :auto-submit-timer="2000"
      @valid="valid = $event"
      @submit="submit"
    >
      <div class="skill-input">
        <PsiInput
          v-model="form.name"
          required
          :max-length="80"
          @focus="onFormFocus"
          @blur="onFormBlur"
        />
        <PsiSlider
          v-model="form.level"
          class="level-slider"
          :max="2"
        />
        <PsiButton
          flat
          icon="close"
          @click="onRemove"
        />
      </div>
    </PsiForm>
  </article>
</template>

<script setup lang="ts">
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import { computed, onMounted, PropType, ref, toRef } from "vue";
import { IResumeSkill, ResumeSkillLevelType } from "@/shared/types/resume.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";
import PsiSlider from "@/shared/PsiUI/components/PsiSlider/PsiSlider.vue";
import ResumeSkillsService from "@/shared/services/ResumeSkillsService.ts";

const props = defineProps({
  modelValue: {
    type: Object as PropType<IResumeSkill>,
    required: true
  }
});

const emit = defineEmits({
  "update:modelValue": (value: IResumeSkill) => true,
  "delete": (value: IResumeSkill) => true
});

const currentSkill = toRef(props, "modelValue");

const { t } = useI18n();
const toaster = useToaster();

const resumeSkillsService = ResumeSkillsService;

const autoSubmitStop = ref(true);
const valid = ref<boolean>(true);

type TForm = {
  name?: string;
  level: ResumeSkillLevelType;
};
const form = ref<TForm>({
  level: ResumeSkillLevelType.Low
});

const createMode = computed(() => currentSkill.value?.id === 0);

onMounted(() => {
  const skill = currentSkill.value!;
  const fm = form.value;
  fm.name = skill.name;
  fm.level = skill.level;
});

async function submit() {
  try {
    const fm = form.value;
    if (createMode.value) {
      const savedSkill = await resumeSkillsService.createResumeSkill({
        resumeId: currentSkill.value?.resumeId,
        name: fm.name!,
        level: +fm.level
      });
      emit("update:modelValue", savedSkill);
    }
    else {
      const savedSkill = await resumeSkillsService.updateResumeSkill({
        id: currentSkill.value?.id,
        name: fm.name!,
        level: +fm.level
      });

      const company = currentSkill.value;
      company.name = savedSkill.name;
      company.level = savedSkill.level;
    }
  }
  catch (error) {
    toaster.error("Произошла ошибка при сохранении навыка");
  }
}

function onFormFocus() {
  autoSubmitStop.value = true;
}

function onFormBlur() {
  autoSubmitStop.value = false;
}

async function onRemove() {
  try {
    await resumeSkillsService.deleteResumeSkill(currentSkill.value!.id);
    emit("delete", currentSkill.value);
  }
  catch (error) {
    toaster.error("Произошла ошибка при удалении навыка");
  }
}

</script>

<style scoped src="./ResumeEditSkill.scss" />
