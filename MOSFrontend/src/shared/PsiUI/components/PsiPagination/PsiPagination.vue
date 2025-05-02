<template>
  <section
    v-if="limit < total"
    class="psi-pagination"
  >
    <PsiButton
      :disabled="previousDisabled"
      flat
      icon="left"
      @click="setPage(currentPage - 1)"
    />

    <template
      v-for="page in visiblePages"
      :key="page.index"
    >
      <PsiButton
        v-if="page.type === 'page'"
        class="page-button"
        :class="{ active: page.index === currentPage }"
        flat
        @click="setPage(page.index)"
      >
        {{ page.index }}
      </PsiButton>
      <span
        v-else
        class="pagination-ellipsis"
      >
        ...
      </span>
    </template>

    <PsiButton
      :disabled="nextDisabled"
      flat
      icon="right"
      @click="setPage(currentPage + 1)"
    />
  </section>
</template>

<script setup lang="ts">
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import { computed, toRef } from "vue";

const props = defineProps({
  currentPage: {
    type: Number,
    default: 1,
    validator(value: number) {
      return value > 0;
    }
  },
  limit: {
    type: Number,
    default: 10,
    validator(value: number) {
      return value > 0;
    }
  },
  total: {
    type: Number,
    required: true,
    validator(value: number) {
      return value >= 0;
    }
  },
  visiblePages: {
    type: Number,
    default: 5,
    validator(value: number) {
      return value >= 0;
    }
  }
});

const emit = defineEmits({
  "update:currentPage": (value: number) => true,
  "selectPage": (offset: number) => true
});

type TPage = { type: "page" | "ellipsis"; index?: number };

const currentPage = toRef(props, "currentPage");

const totalPages = computed(() => Math.ceil(props.total / props.limit));

const previousDisabled = computed(() => {
  return currentPage.value === 1;
});

const nextDisabled = computed(() => {
  return currentPage.value === totalPages.value;
});

const visiblePages = computed(() => {
  const pages: TPage[] = [];

  const total = totalPages.value;

  const borderPagesCount = Math.floor(props.visiblePages / 2);

  let start = Math.max(1, currentPage.value - borderPagesCount);
  let end = start + borderPagesCount * 2;

  if (end > total) {
    end = total;
    start = Math.max(1, end - borderPagesCount * 2);
  }

  // First page
  if (start > 1) {
    pages.push({ type: "page", index: 1 });
  }

  if (start > 2) {
    pages.push({ type: "ellipsis" });
  }

  // Central pages
  for (let i = start; i <= end; i++) {
    pages.push({ type: "page", index: i });
  }

  if (end < total - 1) {
    pages.push({ type: "ellipsis" });
  }

  // Last page
  if (end < total) {
    pages.push({ type: "page", index: totalPages.value });
  }

  return pages;
});

function setPage(page: number) {
  emit("update:currentPage", page);
  emit("selectPage", (page - 1) * props.limit);
}
</script>

<style scoped src="./PsiPagination.scss" />
