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
    <div style="display: contents">
      <div class="card-border mb-3" style="width: max-content">
        <n-button quaternary circle type="primary" @click="onSave">
          <template #icon>
            <n-icon><plus-ico /></n-icon>
          </template>
        </n-button>
        <n-divider vertical style="height: 2em" />
        <n-button quaternary circle type="error" @click="onCancel">
          <template #icon>
            <n-icon><minus-ico /></n-icon>
          </template>
        </n-button>
      </div>
    </div>
    <div class="card-border mb-3">
      <n-form-item label="Name" path="user.name">
        <n-input v-model:value="formValue.user.name" placeholder="Input Name" />
      </n-form-item>
    </div>
  </n-form>
</template>

<script setup lang="ts">
import { ref } from "vue";
import { NForm, NFormItem, NInput, NButton, NDivider, NIcon } from "naive-ui";
import { Check as plusIco, X as minusIco } from "@vicons/tabler";

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
