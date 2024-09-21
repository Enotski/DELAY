<template>
  <n-form
    ref="formRef"
    inline
    :label-width="80"
    :model="formValue"
    size="large"
    class="row"
  >
    <div style="display: contents">
      <div class="card-border mb-3" style="width: max-content">
        <n-button type="primary" @click="onSave"> Save </n-button>
      </div>
    </div>
    <div class="card-border mb-3">
      <n-form-item label="Name" path="name">
        <n-input v-model:value="formValue.name" placeholder="Input name" />
      </n-form-item>
      <n-form-item label="Email" path="email">
        <n-input v-model:value="formValue.email" placeholder="Input email" />
      </n-form-item>
      <n-form-item label="PhoneNumber" path="phoneNumber">
        <n-input
          v-model:value="formValue.phoneNumber"
          placeholder="Input phone"
        />
      </n-form-item>
      <div class="d-flex pe-3">
        <n-form-item style="width: 100%" label="Password" path="password">
          <n-input
            type="password"
            show-password-on="click"
            placeholder="Input password"
            v-model:value="password"
            :maxlength="32"
          >
            <template #password-visible-icon>
              <n-icon :size="16" :component="visibleOnPassIco" />
            </template>
            <template #password-invisible-icon>
              <n-icon :size="16" :component="visibleOffPassIco" />
            </template>
          </n-input>
        </n-form-item>
        <n-button
          style="align-self: center"
          type="primary"
          @click="onSavePassword"
        >
          Save
        </n-button>
      </div>
    </div>
  </n-form>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { NForm, NFormItem, NInput, NButton, NIcon } from "naive-ui";
import {
  Glasses as visibleOffPassIco,
  GlassesOutline as visibleOnPassIco,
} from "@vicons/ionicons5";

import { useUserStore } from "@/stores/user-store";

const authStore = useUserStore();

import RequestUtils from "@/utils/request-utils";

const formValue = ref({
  id: "",
  name: "",
  email: "",
  phoneNumber: "",
});
const password = ref("");

onMounted(async () => {
  await getInfo();
});

async function getInfo() {
  let id = await setInterval(async () => {
    if (authStore.user.id !== "") {
      clearInterval(id);
      await RequestUtils.sendRequest("users", "GET", {
        id: authStore.user.id,
      })
        .then((response) => {
          formValue.value = response;
          password.value = response.password;
        })
        .catch((response) => {
          console.log(response);
        });
    }
  }, 10);
}

async function onSave(e: MouseEvent) {
  await RequestUtils.sendRequest("users", "PUT", formValue.value)
    .then((response) => console.log(response))
    .catch((response) => console.log(response));
}
async function onSavePassword(e: MouseEvent) {
  await RequestUtils.sendRequest("users/password", "PATCH", {
    id: formValue.value.id,
    password: password.value,
  })
    .then((response) => console.log(response))
    .catch((response) => console.log(response));
}
</script>
