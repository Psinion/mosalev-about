<template>
  <PsiDialog
    :visible="visible"
    @update:visible="$emit('update:visible', $event)"
  >
    <template #header>
      Создание проекта
    </template>
  </PsiDialog>
</template>

<script setup lang="ts">
import PsiDialog from "@/shared/PsiUI/components/PsiDialog/PsiDialog.vue";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits({
  "update:visible": (value: boolean) => true
});
</script>

<style scoped src="./ProjectDialog.scss" />
