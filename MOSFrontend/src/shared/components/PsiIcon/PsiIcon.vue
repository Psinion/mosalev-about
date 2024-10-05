<template>
  <oh-vue-icon :name="icon" :scale="scale" />
</template>

<script setup lang="ts">
import { OhVueIcon } from "oh-vue-icons";
import { PropType } from 'vue';
import { TIcon } from '@/shared/components/PsiIcon/types.ts';

defineProps({
    icon: {
        type: String as PropType<TIcon>,
        required: true
    },
    title: {
        type: String,
        default: null
    },
    fill: {
        type: String,
        default: null
    },
    scale: {
        type: Number,
        default: 1,
    },
    animation: {
        type: String,
        default: null
    },
    hover: {
        type: Boolean,
        default: false
    },
    flip: {
        type: Boolean,
        default: false
    },
    speed: {
        type: String,
        default: null
    },
    label: {
        type: String,
        default: null
    },
    inverse: {
        type: Boolean,
        default: false
    }
});

</script>

<style scoped src="./PsiIcon.scss" />
