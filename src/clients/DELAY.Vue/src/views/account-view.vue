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
        <n-button type="primary" @click="onSave">
          <template #icon>
            <n-icon><plus-ico /></n-icon>
          </template>
        </n-button>
        <n-divider vertical style="height: 2em" />
        <n-button type="error" @click="onCancel">
          <template #icon>
            <n-icon><minus-ico /></n-icon>
          </template>
        </n-button>
      </div>
    </div>
    <div class="card-border mb-3">
      <n-form-item label="Name" path="name">
        <n-input v-model:value="formValue.name" placeholder="Input Name" />
      </n-form-item>
    </div>
  </n-form>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { NForm, NFormItem, NInput, NButton, NDivider, NIcon } from "naive-ui";
import { Add as plusIco, Remove as minusIco } from "@vicons/ionicons5";

const formValue = ref({
  name: "",
});
const rules = {
  name: {
    required: true,
    message: "Please input your name",
    trigger: "blur",
  },
};

onMounted(async () => {
  await getInfo();
});

async function getInfo() {
  formValue.value.name = "default name";
  console.log("get info");
}

function onSave(e: MouseEvent) {
  console.log("Save");
}
function onCancel(e: MouseEvent) {
  formValue.value.name = "default name";
  console.log("Cancel");
}
</script>
