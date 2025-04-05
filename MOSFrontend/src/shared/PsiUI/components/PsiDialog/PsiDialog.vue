<template>
  <Transition name="dialog-fade">
    <div
      v-if="visible"
      class="psi-dialog"
      @click.self="closeWhenOutsideClick"
    >
      <div class="dialog-container">
        <div class="dialog-header">
          <h2>
            <slot name="header" />
          </h2>
        </div>

        <div class="dialog-body">
          <slot />
        </div>

        <div class="dialog-footer">
          <slot name="footer">
            <PsiButton
              class="cancel-button"
              @click="close"
            >
              {{ t("psiUi.psiDialog.cancelButton") }}
            </PsiButton>
            <PsiButton
              class="confirm-button"
              @click="confirm"
            >
              {{ t("psiUi.psiDialog.confirmButton") }}
            </PsiButton>
          </slot>
        </div>
      </div>
    </div>
  </Transition>
</template>

<script setup lang="ts">
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import { useI18n } from "vue-i18n";
import { watch } from "vue";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  hideWhenClickOutside: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits({
  "update:visible": (value: boolean) => true,
  "close": () => true,
  "confirm": () => true
});

const { t } = useI18n();

function close() {
  emit("update:visible", false);
  emit("close");
}

function closeWhenOutsideClick() {
  if (props.hideWhenClickOutside) {
    emit("update:visible", false);
    emit("close");
  }
}

function confirm() {
  emit("update:visible", false);
  emit("confirm");
}

/**
 * Убрать возможность скролла во время открытого диалогового окна.
 * @param value
 */
function setRootOverflow(value: boolean) {
  if (value) {
    setTimeout(() => document.body.classList.toggle("no-overflow", false), 200);
  }
  else {
    document.body.classList.toggle("no-overflow", true);
  }
}

watch(() => props.visible, (value) => {
  setRootOverflow(!value);
});
</script>

<style scoped src="./PsiDialog.scss" />
