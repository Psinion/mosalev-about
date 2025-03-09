<template>
  <article class="resume-edit-posts-list">
    <h3>Должности</h3>
    <section class="posts">
      <div
        v-for="post in modelValue"
        :key="post.id"
        class="post"
      >
        <ResumeEditPost
          :model-value="post"
          :company-id="companyId"
          :removable="canRemovePost"
        />
      </div>
    </section>
    <div class="company-actions">
      <PsiButton
        :disabled="!companyId"
        @click="addPost"
      >
        Добавить должность
      </PsiButton>
    </div>
  </article>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import { computed, PropType, toRef } from "vue";
import { ResumeCompanyEntryPost } from "@/shared/types/resume.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import ResumeEditPost
  from "@/modules/resume/pages/ResumePage/components/ResumeEdit/components/ResumeEditPost/ResumeEditPost.vue";
import { date2DateOnly } from "@/shared/utils/helpers.ts";

const props = defineProps({
  modelValue: {
    type: Array as PropType<ResumeCompanyEntryPost[]>,
    default: () => []
  },
  companyId: {
    type: Number,
    default: null
  }
});

const emit = defineEmits({
  "update:modelValue": (value: ResumeCompanyEntryPost[]) => true
});

const postsList = toRef(props, "modelValue");

const { t } = useI18n();

const canRemovePost = computed(() => postsList.value.length > 1);

function addPost() {
  const posts = postsList.value;
  posts.push({
    id: 0,
    companyId: 0,
    name: undefined,
    dateStart: date2DateOnly(new Date())
  });
  emit("update:modelValue", posts);
}

function removePost(item: ResumeCompanyEntryPost) {
  const posts = postsList.value;
  const itemIndex = posts.indexOf(item);
  if (itemIndex > -1) {
    posts.splice(itemIndex, 1);
    emit("update:modelValue", posts);
  }
}
</script>

<style scoped src="./ResumeEditPostsList.scss" />
