<template>
  <form
    class="psi-form"
    @submit.prevent="onSubmit"
  >
    <slot :valid="valid" />
  </form>
</template>

<script setup lang="ts">
import { useForm, useIsFormValid } from "vee-validate";
import { ref, watch } from "vue";

const emit = defineEmits({
  submit: () => true,
  valid: (value: boolean) => true
});

const form = useForm();
const isValid = useIsFormValid();

const valid = ref(true);

async function onSubmit() {
  await form.validate();
  if (!isValid.value) {
    return;
  }

  emit("submit");
}

async function validate() {
  await form.validate();
  console.log(isValid.value);
  return isValid.value;
}

watch(() => form.meta.value.valid, (value) => {
  valid.value = value;
  console.log(value);
  emit("valid", value);
});

defineExpose({
  validate
});
</script>
