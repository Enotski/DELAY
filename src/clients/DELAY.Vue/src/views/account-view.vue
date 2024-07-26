<template>
  <n-form
    ref="formRef"
    inline
    :label-width="80"
    :model="formValue"
    :rules="rules"
    size="large"
  >
    <n-form-item label="Name" path="user.name">
      <n-input v-model:value="formValue.user.name" placeholder="Input Name" />
    </n-form-item>
    <n-form-item>
      <n-button type="success" ghost @click="onSave"> Save </n-button>
      <n-button type="error" ghost @click="onCancel"> Cancel </n-button>
    </n-form-item>
  </n-form>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { NForm, NFormItem, NInput, NButton } from "naive-ui";

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
