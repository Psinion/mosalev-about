<template>
  <PsiDialog
    :visible="visible"
    hide-when-click-outside
    @update:visible="$emit('update:visible', $event)"
    @confirm="save"
  >
    <template #header>
      {{ t('articles.dialogDelete.title') }}
    </template>

    <div class="project-delete-dialog">
      {{ t('articles.dialogDelete.description') }}
    </div>
  </PsiDialog>
</template>

<script setup lang="ts">
import PsiDialog from "@/shared/PsiUI/components/PsiDialog/PsiDialog.vue";
import { useI18n } from "vue-i18n";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { PropType, toRef } from "vue";
import ArticlesServiceInstance from "@/shared/services/ArticlesService.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { RouteNames } from "@/router/routeNames.ts";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  articleId: {
    type: [Number, null] as PropType<number | null>,
    default: null
  }
});

const emit = defineEmits({
  "update:visible": (value: boolean) => true,
  "delete": (value: number) => true
});

const toaster = useToaster();
const { t } = useI18n();
const articlesService = ArticlesServiceInstance;

const articleId = toRef(props, "articleId");

async function save() {
  if (!articleId.value) {
    return;
  }

  try {
    await articlesService.deleteArticle(articleId.value);
    emit("delete", articleId.value);
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
}
</script>

<style scoped src="./ArticleDeleteDialog.scss" />
