<template>
  <PsiForm
    v-model:auto-submit-timer-stop="autoSubmitStop"
    class="resume-edit-post"
    :auto-submit-timer="2000"
    @valid="valid = $event"
    @submit="submit"
  >
    <div class="post-input">
      <PsiInput
        v-model="form.name"
        label="Название"
        @focus="onFormFocus"
        @blur="onFormBlur"
      />
      <PsiButton
        flat
        icon="close"
        :disabled="!removable"
        @click="removePost(post)"
      />
    </div>
    <div class="date-period">
      <PsiInputDate
        v-model="form.dateStart"
        label="Дата начала"
        required
        @focus="onFormFocus"
        @blur="onFormBlur"
      />
      <PsiInputDate
        v-model="form.dateEnd"
        label="Дата окончания"
        @focus="onFormFocus"
        @blur="onFormBlur"
      />
    </div>
    <PsiTextarea
      v-model="form.description"
      label="Описание"
      resizable="vertical"
    />
  </PsiForm>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiTextarea from "@/shared/PsiUI/components/PsiTextarea/PsiTextarea.vue";
import { computed, onMounted, PropType, ref, toRef } from "vue";
import { ResumeCompanyEntryPost } from "@/shared/types/resume.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PsiInputDate from "@/shared/PsiUI/components/PsiInputDate/PsiInputDate.vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";
import ResumePostsServiceInstance from "@/shared/services/ResumePostsService.ts";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";

const props = defineProps({
  modelValue: {
    type: Object as PropType<ResumeCompanyEntryPost>,
    required: true
  },
  removable: {
    type: Boolean,
    default: false
  },
  companyId: {
    type: Number,
    default: null
  }
});

const emit = defineEmits({
  "update:modelValue": (value: ResumeCompanyEntryPost) => true
});

const { t } = useI18n();
const toaster = useToaster();
const resumePostsService = ResumePostsServiceInstance;

const post = toRef(props, "modelValue");
const companyId = toRef(props, "companyId");

const autoSubmitStop = ref(true);
const valid = ref(true);

type TForm = {
  name?: string;
  dateStart?: string | Date;
  dateEnd?: string | Date;
  description?: string;
};
const form = ref<TForm>({});

const createMode = computed(() => post.value?.id === 0);

onMounted(() => {
  const p = post.value!;
  const fm = form.value;
  fm.name = p.name;
  fm.dateStart = p.dateStart ?? undefined;
  fm.dateEnd = p.dateEnd ?? undefined;
  fm.description = p.description ?? undefined;
});

async function submit() {
  try {
    const fm = form.value;
    if (createMode.value) {
      const savedPost = await resumePostsService.createResumePost({
        resumeCompanyEntryId: companyId.value,
        name: fm.name!,
        description: fm.description,
        dateStart: fm.dateStart,
        dateEnd: fm.dateEnd
      });
      emit("update:modelValue", savedPost);
    }
    else {
      const savedPost = await resumePostsService.updateResumePost({
        id: post.value!.id,
        name: fm.name!,
        description: fm.description,
        dateStart: fm.dateStart,
        dateEnd: fm.dateEnd
      });
      emit("update:modelValue", savedPost);
    }
  }
  catch (error) {
    toaster.error("Произошла ошибка при сохранении компании");
  }
}

function onFormFocus() {
  autoSubmitStop.value = true;
}

function onFormBlur() {
  autoSubmitStop.value = false;
}
</script>

<style scoped src="./ResumeEditPost.scss" />
