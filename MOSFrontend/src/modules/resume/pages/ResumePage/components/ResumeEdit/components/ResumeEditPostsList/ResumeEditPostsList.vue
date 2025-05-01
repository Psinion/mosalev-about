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
          @create="onCreatePost"
          @update="onUpdatePost"
          @remove="onRemovePost"
        />
      </div>
    </section>
    <div class="company-actions">
      <PsiButton
        :disabled="!canAddPost"
        @click="addPost"
      >
        Новая должность
      </PsiButton>
    </div>
  </article>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import { computed, onMounted, PropType, toRef } from "vue";
import { IResumeCompanyEntryPost } from "@/shared/types/resume.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import ResumeEditPost
  from "@/modules/resume/pages/ResumePage/components/ResumeEdit/components/ResumeEditPost/ResumeEditPost.vue";
import { date2DateOnly } from "@/shared/utils/helpers/helpers.ts";
import ResumePostsServiceInstance from "@/shared/services/ResumePostsService.ts";

const props = defineProps({
  modelValue: {
    type: Array as PropType<IResumeCompanyEntryPost[]>,
    default: () => []
  },
  companyId: {
    type: Number,
    default: null
  }
});

const emit = defineEmits({
  "update:modelValue": (value: IResumeCompanyEntryPost[]) => true
});

const postsList = toRef(props, "modelValue");

const { t } = useI18n();
const resumePostsService = ResumePostsServiceInstance;

const canAddPost = computed(() => props.companyId && !postsList.value.find(x => x.id === 0));
const canRemovePost = computed(() => postsList.value.length > 1);

onMounted(() => {
  if (postsList.value.length === 0) {
    addPost();
  }
});

function addPost() {
  const posts = postsList.value;
  posts.push({
    id: 0,
    resumeCompanyEntryId: 0,
    name: undefined
  });
  emit("update:modelValue", posts);
}

function onCreatePost(item: IResumeCompanyEntryPost) {
  const posts = postsList.value;
  const foundIndex = posts.findIndex(x => x.id === 0);
  if (foundIndex >= 0) {
    posts[foundIndex] = item;
  }
  emit("update:modelValue", posts);
}

function onUpdatePost(item: IResumeCompanyEntryPost) {
  const posts = postsList.value;
  const foundIndex = posts.findIndex(x => x.id === item.id);
  if (foundIndex) {
    posts[foundIndex] = item;
  }
  emit("update:modelValue", posts);
}

function onRemovePost(item: IResumeCompanyEntryPost) {
  const posts = postsList.value;
  const itemIndex = posts.indexOf(item);
  if (itemIndex > -1) {
    posts.splice(itemIndex, 1);
    emit("update:modelValue", posts);
  }
}
</script>

<style scoped src="./ResumeEditPostsList.scss" />
