<template>
  <PsiForm
    class="resume-edit-post"
    :auto-submit-timer="2000"
    @valid="valid = $event"
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
import { onMounted, PropType, ref, toRef } from "vue";
import { TResumeCompanyEntryPost } from "@/shared/types/resume.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PsiInputDate from "@/shared/PsiUI/components/PsiInputDate/PsiInputDate.vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";

const props = defineProps({
  modelValue: {
    type: Object as PropType<TResumeCompanyEntryPost>,
    required: true
  },
  removable: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits({
  "update:modelValue": (value: TResumeCompanyEntryPost) => true
});

const post = toRef(props, "modelValue");

const autoSubmitStop = ref(true);
const valid = ref(true);

type TForm = {
  name?: string;
  dateStart?: string | Date;
  dateEnd?: string | Date;
  description?: string;
};
const form = ref<TForm>({});

const { t } = useI18n();

onMounted(() => {
  const p = post.value!;
  const fm = form.value;
  fm.name = p.name;
  fm.dateStart = p.dateStart ?? undefined;
  fm.dateEnd = p.dateEnd ?? undefined;
  fm.description = p.description ?? undefined;
});

function onFormFocus() {
  autoSubmitStop.value = true;
}

function onFormBlur() {
  autoSubmitStop.value = false;
}
</script>

<style scoped src="./ResumeEditPost.scss" />
