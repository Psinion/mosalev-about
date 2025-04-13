<template>
  <section class="project-card">
    <h3>{{ project.title }}</h3>
    <div>
      {{ project.description }}
    </div>
    <footer>
      <div class="hint-regular">
        {{ dateUpdate }}
      </div>
    </footer>
  </section>
</template>

<script setup lang="ts">

import { computed, PropType } from "vue";
import { IProject } from "@/shared/types";

const props = defineProps({
  project: {
    type: Object as PropType<IProject>,
    required: true
  }
});

const dateUpdate = computed(() => {
  const date = props.project.updatedAt ?? props.project.createdAt;
  const newDate = new Date(date);
  return `${newDate.getFullYear()}.${newDate.getMonth().toString().padStart(2, "0")}.${newDate.getDate().toString().padStart(2, "0")}`
    + ` ${newDate.getHours().toString().padStart(2, "0")}:${newDate.getMinutes().toString().padStart(2, "0")}`;
});
</script>

<style scoped src="./ProjectCard.scss" />
