<template>
  <Transition name="dialog-fade">
    <div
      v-if="visible"
      class="psi-dialog"
      @click.self="closeWhenOutsideClick"
    >
      <PsiForm
        v-if="psiForm"
        v-slot="{ valid }"
        class="dialog-container"
        :class="[size]"
        @submit="confirm"
      >
        <div
          class="dialog-header"
        >
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
              native-type="submit"
              :disabled="!valid"
            >
              {{ t("psiUi.psiDialog.confirmButton") }}
            </PsiButton>
          </slot>
        </div>
      </PsiForm>

      <div
        v-else
        class="dialog-container"
        :class="[size]"
      >
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
import { PropType, watch } from "vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";
import { TPsiDialogSize } from "@/shared/PsiUI/components/PsiDialog/types.ts";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  hideWhenClickOutside: {
    type: Boolean,
    default: false
  },
  psiForm: {
    type: Boolean,
    default: false
  },
  size: {
    type: String as PropType<TPsiDialogSize>,
    default: "S"
  }
});

const emit = defineEmits({
  "update:visible": (value: boolean) => true,
  "close": () => true,
  "confirm": () => true,
  "open": () => true
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
  if (value) {
    emit("open");
  }
});
</script>

<style scoped src="./PsiDialog.scss" />
