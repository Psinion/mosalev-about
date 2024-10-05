<template>
  <section class="flat-layout">
    <header>
      <LayoutHeader />
    </header>

    <article  :style="[{'justify-content': alignHorizontal}, {'align-items': alignVertical}]">
      <slot />
    </article>

    <footer>
      <LayoutFooter/>
    </footer>
  </section>
</template>

<script setup lang="ts">
import LayoutHeader from "@/layouts/components/LayoutHeader/LayoutHeader.vue";
import LayoutFooter from '@/layouts/components/LayoutFooter/LayoutFooter.vue';
import { PropType } from 'vue';
import { TFlatLayoutAlignHorizontal, TFlatLayoutAlignVertical } from './types.ts';

defineProps({
  alignHorizontal: {
    type: String as PropType<TFlatLayoutAlignHorizontal>,
    default: 'normal'
  },
  alignVertical: {
    type: String as PropType<TFlatLayoutAlignVertical>,
    default: 'normal'
  }
});

</script>

<style scoped src="./FlatLayout.scss" />
