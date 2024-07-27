<template>
  <n-form
    ref="formRef"
    inline
    :label-width="80"
    :model="formValue"
    :rules="rules"
    size="large"
    class="row"
  >
    <n-card
      class="box-shadow mb-3 shadow"
      :segmented="{
        content: true,
        footer: 'soft',
      }"
    >
      <n-form-item class="w-50" label="Name" path="user.name">
        <n-input v-model:value="formValue.user.name" placeholder="Input Name" />
      </n-form-item>
    </n-card>
    <div style="display: contents">
      <n-card class="box-shadow mb-3 shadow" style="width: max-content">
        <n-button type="success" ghost @click="onSave"> Save </n-button>
        <n-divider vertical style="height: 2em" />
        <n-button type="error" ghost @click="onCancel"> Cancel </n-button>
      </n-card>
    </div>
  </n-form>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { NForm, NCard, NFormItem, NInput, NButton, NDivider } from "naive-ui";

const formValue = ref({
  user: {
    name: "",
    age: "",
  },
  phone: "",
});
const rules = {
  user: {
    name: {
      required: true,
      message: "Please input your name",
      trigger: "blur",
    },
  },
};

function onSave(e: MouseEvent) {
  console.log("Save");
}
function onCancel(e: MouseEvent) {
  console.log("Cancel");
}
</script>
