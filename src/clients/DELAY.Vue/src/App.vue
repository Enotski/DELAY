<template>
  <n-message-provider>
    <n-dialog-provider>
      <n-modal
        v-model:show="showSignInModal"
        preset="dialog"
        title="SignIn / SingUp"
        positive-text="Confirm"
        negative-text="Cancel"
        @positive-click="onConfirmSignInClick"
        @negative-click="onCancelSignInClick"
      >
        <n-form
          ref="formRef"
          inline
          :label-width="80"
          :model="formValue"
          :rules="addRules"
          size="large"
          class="row"
        >
          <n-radio-group
            v-model:value="radioSignModalTypeGroupValue"
            name="radiogroupSingType"
          >
            <n-radio-button value="signIn"> Sign In </n-radio-button>
            <n-radio-button value="signUp"> Sign Up </n-radio-button>
          </n-radio-group>
          <n-form-item
            v-if="radioSignModalTypeGroupValue == 'signUp'"
            :style="{ width: '100%' }"
            path="name"
          >
            <n-input-group>
              <n-input-group-label>Name</n-input-group-label>
              <n-input
                :style="{ height: '34px' }"
                v-model:value="formValue.name"
              />
            </n-input-group>
          </n-form-item>
          <n-form-item>
            <n-radio-group
              v-model:value="radioSignInTypeGroupValue"
              name="radiogroupLoginType"
            >
              <n-radio-button value="email"> Email </n-radio-button>
              <n-radio-button value="phone"> Phone </n-radio-button>
            </n-radio-group>
          </n-form-item>
          <n-form-item
            :style="{ width: '100%' }"
            v-if="radioSignInTypeGroupValue == 'email'"
            path="email"
          >
            <n-input-group
              ><n-input-group-label>Email</n-input-group-label>
              <n-input
                :style="{ height: '34px' }"
                v-model:value="formValue.email"
              />
            </n-input-group>
          </n-form-item>
          <n-form-item
            :style="{ width: '100%' }"
            v-if="radioSignInTypeGroupValue == 'phone'"
            path="phoneNumber"
          >
            <n-input-group
              ><n-input-group-label>Phone</n-input-group-label>
              <n-input
                :style="{ height: '34px' }"
                v-model:value="formValue.phoneNumber"
            /></n-input-group>
          </n-form-item>
          <n-form-item :style="{ width: '100%' }" path="password">
            <n-input-group
              ><n-input-group-label>Password</n-input-group-label>
              <n-input
                :style="{ height: '34px' }"
                v-model:value="formValue.password"
              />
            </n-input-group>
          </n-form-item>
        </n-form>
        <div>
          <n-button-group>
            <n-button type="success" round @click="loginGoogle">
              <template #icon>
                <n-icon><google-ico /></n-icon>
              </template>
            </n-button>
            <n-button type="info" round @click="loginVk">
              <template #icon>
                <n-icon><vk-ico /></n-icon>
              </template>
            </n-button>
          </n-button-group>
        </div>
      </n-modal>
      <div class="flex-container">
        <nav
          class="navbar d-flex fixed-top bg-light box-shadow shadow p-3 py-2"
        >
          <div>
            <n-menu
              :options="menuOptions"
              :value="currentRouteName"
              mode="horizontal"
              responsive
            />
          </div>
          <div>
            <n-button type="success" @click="onSignInModalOpen">
              <template #icon>
                <n-icon><user-ico /></n-icon>
              </template>
              Sign In
            </n-button>
          </div>
        </nav>
        <div class="content-container flex-stretch">
          <RouterView />
        </div>
      </div>
    </n-dialog-provider>
  </n-message-provider>
</template>

<script setup lang="ts">
import type { Component } from "vue";
import { computed, h, ref } from "vue";
import { RouterLink, RouterView } from "vue-router";
import {
  NMenu,
  NIcon,
  NMessageProvider,
  NDialogProvider,
  NButton,
  NButtonGroup,
  NModal,
  NForm,
  NFormItem,
  NInput,
  NInputGroup,
  NInputGroupLabel,
  NRadioButton,
  NRadioGroup,
} from "naive-ui";
import type { MenuOption } from "naive-ui";
import { useRouter } from "vue-router";
import { sendRequest } from "@/utils/request-utils";
import {
  HomeOutline as homeIco,
  PersonOutline as userIco,
  PersonCircleOutline as accountIco,
  PersonAdd as usersIco,
  ChatbubblesOutline as messageIco,
  ClipboardOutline as boardIco,
  LogoGoogle as googleIco,
  LogoVk as vkIco,
} from "@vicons/ionicons5";

import { googleSdkLoaded } from "vue3-google-login";
import {
  Config,
  Auth,
  ConfigAuthMode,
  ConfigResponseMode,
  type AuthError,
} from "@vkid/sdk";

const clientid = import.meta.env.VITE_GOOGLE_CLIENT_ID;
const vkclientid = import.meta.env.VITE_VK_CLIENT_ID;

function renderIcon(icon: Component) {
  return () => h(NIcon, null, { default: () => h(icon) });
}

const router = useRouter();
const currentRouteName: any = computed(() => router.currentRoute.value.name);
const isAuthorized = ref(true);

const showSignInModal = ref(false);
const radioSignInTypeGroupValue = ref("email");
const radioSignModalTypeGroupValue = ref("signIn");

const formValue = ref({
  name: "",
  email: "",
  phoneNumber: "",
  password: "",
});

const addRules = {
  name: {
    required: true,
    message: "Please input your name",
    trigger: "blur",
  },
  password: {
    required: true,
    message: "Please input your password",
    trigger: "blur",
  },
};

const loginGoogle = () => {
  googleSdkLoaded((google) => {
    google.accounts.oauth2
      .initCodeClient({
        client_id: clientid,
        scope: "email profile openid",
        state: "supernumber",
        redirect_uri: "https://localhost:443",
        callback: async (response) => {
          console.log("Google response", response);
          await sendRequest("auth/signin-google", "POST", {
            code: response.code,
          }).then((value) => {
            console.log(value);
          });
        },
      })
      .requestCode();
  });
};

const loginVk = () => {
  Config.init({
    app: vkclientid, // Идентификатор приложения.
    redirectUrl: "https://localhost:443", // Адрес для перехода после авторизации.
    state: "dj29fnsadjsd82", // Произвольная строка состояния приложения.
    codeVerifier: "FGH767Gd65", // Верификатор в виде случайной строки. Обеспечивает защиту передаваемых данных.
    scope: "email",
    mode: ConfigAuthMode.InNewWindow,
    responseMode: ConfigResponseMode.Callback,
  });

  Auth.login()
    .then(async (response: any) => {
      console.log("Vk response", response);
      await sendRequest("auth/signin-vk", "POST", {
        code: response.code,
        deviceId: response.device_id,
      }).then((value) => {
        console.log(value);
      });
    })
    .catch((e: AuthError) => {
      console.error("Ошибка Auth.login()", e);
    });
};

function onSignInModalOpen() {
  showSignInModal.value = true;
}

async function onConfirmSignInClick() {
  if (radioSignModalTypeGroupValue.value == "signIn") {
    await sendRequest("auth/signin-form", "POST", formValue.value).then(
      (response) => {
        console.log(response);
      }
    );
  } else {
    await sendRequest("auth/signup-form", "POST", formValue.value).then(
      (response) => {
        console.log(response);
      }
    );
  }

  showSignInModal.value = false;
}

function onCancelSignInClick() {
  showSignInModal.value = false;
}

const menuOptions: MenuOption[] = [
  {
    label: () =>
      h(
        RouterLink,
        {
          to: "/",
        },
        { default: () => "Home" }
      ),
    key: "home",
    icon: renderIcon(homeIco),
  },
  {
    label: () =>
      h(
        RouterLink,
        {
          to: "/tickets",
        },
        { default: () => "Tickets" }
      ),
    show: isAuthorized.value,
    key: "tickets",
    icon: renderIcon(boardIco),
  },
  {
    label: () =>
      h(
        RouterLink,
        {
          to: "/rooms",
        },
        { default: () => "ChatRooms" }
      ),
    show: isAuthorized.value,
    key: "rooms",
    icon: renderIcon(messageIco),
  },
  {
    label: () =>
      h(
        RouterLink,
        {
          to: "/users",
        },
        { default: () => "Users" }
      ),
    show: isAuthorized.value,
    key: "users",
    icon: renderIcon(usersIco),
  },
  {
    label: () =>
      h(
        RouterLink,
        {
          to: "/account",
        },
        { default: () => "Account" }
      ),
    show: isAuthorized.value,
    key: "account",
    icon: renderIcon(accountIco),
  },
];
</script>
