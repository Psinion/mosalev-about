<template>
  <article class="resume-edit-posts-list">
    <h3>Должности</h3>
    <section class="posts">
      <div
        v-for="post in modelValue"
        :key="post.id"
        class="post"
      >
        <div class="post-input">
          <PsiInput
            v-model="post.name"
            label="Название"
          />
          <PsiButton
            flat
            icon="close"
            :disabled="!canRemovePost"
            @click="removePost(post)"
          />
        </div>
        <div class="date-period">
          <PsiInputDate
            v-model="post.dateStart"
            label="Дата начала"
            required
          />
          <PsiInputDate
            v-model="post.dateEnd"
            label="Дата окончания"
          />
        </div>
        <PsiTextarea
          v-model="post.description"
          label="Описание"
          resizable="vertical"
        />
      </div>
    </section>
    <div class="company-actions">
      <PsiButton @click="addPost">
        Добавить должность
      </PsiButton>
    </div>
  </article>
</template>

<script setup lang="ts">
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiTextarea from "@/shared/PsiUI/components/PsiTextarea/PsiTextarea.vue";
import { computed, PropType, toRef } from "vue";
import { TResumeCompanyEntryPost } from "@/shared/types/resume.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PsiInputDate from "@/shared/PsiUI/components/PsiInputDate/PsiInputDate.vue";

const props = defineProps({
  modelValue: {
    type: Array as PropType<TResumeCompanyEntryPost[]>,
    default: () => []
  }
});

const emit = defineEmits({
  "update:modelValue": (value: TResumeCompanyEntryPost[]) => true
});

const postsList = toRef(props, "modelValue");

const { t } = useI18n();
const toaster = useToaster();

const canRemovePost = computed(() => postsList.value.length > 1);

function addPost() {
  const posts = postsList.value;
  posts.push({
    id: 0,
    companyId: 0,
    name: "",
    dateStart: new Date()
  });
  emit("update:modelValue", posts);
}

function removePost(item: TResumeCompanyEntryPost) {
  const posts = postsList.value;
  const itemIndex = posts.indexOf(item);
  if (itemIndex > -1) {
    posts.splice(itemIndex, 1);
    emit("update:modelValue", posts);
  }
}
</script>

<style scoped src="./ResumeEditPostsList.scss" />
