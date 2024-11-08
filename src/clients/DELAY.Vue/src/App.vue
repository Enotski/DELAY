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
          <div class="d-flex">
            <div v-if="isDisplaySignBtn" class="mx-5">
              <n-button
                v-if="!isAuthorized"
                type="success"
                @click="onSignInModalOpen"
              >
                <template #icon>
                  <n-icon><user-ico /></n-icon>
                </template>
                Sign In
              </n-button>
            </div>
            <div v-if="isAuthorized">
              <n-tag size="large" type="success"
                >{{ userStore.user.name }}
                <template #icon>
                  <n-button
                    title="Sign Out"
                    size="small"
                    quaternary
                    type="success"
                    @click="onSignOut"
                  >
                    <template #icon>
                      <n-icon><signout-ico /></n-icon>
                    </template>
                  </n-button>
                </template>
              </n-tag>
            </div>
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
import { computed, h, ref, onMounted } from "vue";
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
  NTag,
} from "naive-ui";
import type { MenuOption } from "naive-ui";
import router from "./router";
import { useUserStore } from "@/stores/user-store";
import { RequestUtils } from "@/utils";

import { makeId } from "@/utils/random-generator";
import FingerprintJS from "@fingerprintjs/fingerprintjs";
import { googleSdkLoaded } from "vue3-google-login";

import {
  HomeOutline as homeIco,
  PersonOutline as userIco,
  PersonCircleOutline as accountIco,
  PersonAdd as usersIco,
  ChatbubblesOutline as messageIco,
  ClipboardOutline as boardIco,
  LogoGoogle as googleIco,
  LogoVk as vkIco,
  ExitOutline as signoutIco,
} from "@vicons/ionicons5";
import {
  Config,
  Auth,
  ConfigAuthMode,
  ConfigResponseMode,
  type AuthError,
} from "@vkid/sdk";

const userStore = useUserStore();

const tokenClaimsScheme =
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims";

const tokenPayloadNames = {
  role: "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
  email: tokenClaimsScheme + "/emailaddress",
  phone: tokenClaimsScheme + "/mobilephone",
  name: tokenClaimsScheme + "/name",
  id: "ueid",
};

const fpPromise = FingerprintJS.load();
let fingerprint = "";

const clientid = import.meta.env.VITE_GOOGLE_CLIENT_ID;
const vkclientid = import.meta.env.VITE_VK_CLIENT_ID;
const authRedirectUri = import.meta.env.VITE_AUTH_REDIRECT_URI;

function renderIcon(icon: Component) {
  return () => h(NIcon, null, { default: () => h(icon) });
}

const currentRouteName: any = computed(() => router.currentRoute.value.name);
const isAuthorized = ref<boolean>(false);
const isDisplaySignBtn = ref<boolean>(false);

const showSignInModal = ref<boolean>(false);
const radioSignInTypeGroupValue = ref("email");
const radioSignModalTypeGroupValue = ref("signIn");

RequestUtils.setTokensRefreshFailedCallBack(() => {
  userStore.clearUser();
  isAuthorized.value = false;
  console.log("silent refresh failed");
});

function setUserData(data: any) {
  isAuthorized.value = true;

  let tokenPayload = RequestUtils.parseJwt(data.tokens.accessToken);
  console.log(tokenPayload);

  userStore.setUser({
    role: tokenPayload[tokenPayloadNames.role],
    email: tokenPayload[tokenPayloadNames.email],
    phone: tokenPayload[tokenPayloadNames.phone],
    name: tokenPayload[tokenPayloadNames.name],
    id: tokenPayload[tokenPayloadNames.id],
  });

  setMenuOptions(data.endpoints);
  console.log(userStore.user);
}

function onSuccessAuth(result: any) {
  showSignInModal.value = false;

  RequestUtils.setAccessToken(result.tokens.accessToken);
  setUserData(result);
}
const formValue = ref({
  name: "",
  email: "",
  phoneNumber: "",
  password: "",
  fingerprint: "",
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

onMounted(async () => {
  if (fingerprint === "") await setFingerprint();

  if (isAuthorized.value == false) {
    await RequestUtils.default
      .sendRequest("auth/signin-transient", "GET", {
        fingerprint: fingerprint,
      })
      .then((response) => {
        onSuccessAuth(response);
      })
      .finally(() => {
        isDisplaySignBtn.value = true;
      });
  }
});

const loginGoogle = () => {
  googleSdkLoaded((google) => {
    google.accounts.oauth2
      .initCodeClient({
        client_id: clientid,
        scope: "email profile openid",
        state: "supernumber",
        redirect_uri: authRedirectUri,
        callback: async (response) => {
          console.log("Google response", response);

          if (fingerprint === "") await setFingerprint();

          await RequestUtils.default
            .sendRequest("auth/signin-google", "POST", {
              code: response.code,
              fingerprint: fingerprint,
            })
            .then((response) => {
              onSuccessAuth(response);
            });
        },
      })
      .requestCode();
  });
};

const loginVk = () => {
  let codeVerifier = makeId(10);

  Config.init({
    app: vkclientid, // Идентификатор приложения.
    redirectUrl: authRedirectUri, // Адрес для перехода после авторизации.
    state: makeId(16), // Произвольная строка состояния приложения.
    codeVerifier: codeVerifier, // Верификатор в виде случайной строки. Обеспечивает защиту передаваемых данных.
    scope: "email",
    mode: ConfigAuthMode.InNewWindow,
    responseMode: ConfigResponseMode.Callback,
  });

  Auth.login()
    .then(async (response: any) => {
      console.log("Vk response", response);

      if (fingerprint === "") await setFingerprint();

      await RequestUtils.default
        .sendRequest("auth/signin-vk", "POST", {
          code: response.code,
          deviceId: response.device_id,
          codeVerifier: codeVerifier,
          fingerprint: fingerprint,
        })
        .then((response) => {
          onSuccessAuth(response);
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
  if (fingerprint === "") await setFingerprint();

  formValue.value.fingerprint = fingerprint;

  if (radioSignModalTypeGroupValue.value == "signIn") {
    await RequestUtils.default
      .sendRequest("auth/signin", "POST", formValue.value)
      .then((response) => {
        onSuccessAuth(response);
      });
  } else {
    await RequestUtils.default
      .sendRequest("auth/signup", "POST", formValue.value)
      .then((response) => {
        onSuccessAuth(response);
      });
  }
}
function onCancelSignInClick() {
  showSignInModal.value = false;
}

async function onSignOut() {
  userStore.clearUser();
  RequestUtils.clearAccessToken();
  isAuthorized.value = false;
  setMenuOptions([]);

  router.push("/");

  await RequestUtils.default
    .sendRequest("auth/signout", "POST")
    .catch((error) => {
      console.log(error);
    });
}

async function setFingerprint() {
  let fp = await fpPromise;
  let result = await fp.get();
  fingerprint = result.visitorId;
}

function setMenuOptions(endpoints: []) {
  menuOptions.value = [
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
  ];

  endpoints.forEach((element: any) => {
    menuOptions.value.push({
      label: () =>
        h(
          RouterLink,
          {
            to: "/" + element.path,
          },
          { default: () => element.title }
        ),
      key: element.path,
      show: isAuthorized.value,
      icon: renderIcon(getRouteItemIcon(element.path)),
    });
  });
}

function getRouteItemIcon(path: string): Component {
  switch (path) {
    case "boards":
      return boardIco;
    case "rooms":
      return messageIco;
    case "users":
      return usersIco;
    case "account":
      return accountIco;
    default:
      return homeIco;
  }
}

const menuOptions = ref<MenuOption[] | any[]>([
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
]);
</script>
