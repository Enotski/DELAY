<template>
  <n-message-provider>
    <n-dialog-provider>
      <n-modal
        v-model:show="showModal"
        preset="dialog"
        title="Dialog"
        content="Are you sure?"
        positive-text="Confirm"
        negative-text="Cancel"
        @positive-click="onPositiveClick"
        @negative-click="onNegativeClick"
      />
      <div class="flex-container">
        <nav
          class="navbar d-flex fixed-top bg-light justify-content-between box-shadow shadow p-3 py-2"
        >
          <n-menu
            :options="menuOptions"
            :value="currentRouteName"
            mode="horizontal"
            responsive
          />
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
  NModal,
} from "naive-ui";
import type { MenuOption } from "naive-ui";
import { useRouter } from "vue-router";

import {
  SmartHome as homeIco,
  UserCircle as accountIco,
  Users as usersIco,
  Messages as messageIco,
  ClipboardList as boardIco,
} from "@vicons/tabler";

function renderIcon(icon: Component) {
  return () => h(NIcon, null, { default: () => h(icon) });
}

const router = useRouter();
const currentRouteName: any = computed(() => router.currentRoute.value.name);
const isAuthorized = ref(true);
const showModal = ref(false);

function auth() {
  showModal.value = true;
  console.log("boardInfo");
}
function onPositiveClick(row: any) {
  console.log("onPositiveClick");
}
function onNegativeClick(row: any) {
  showModal.value = false;
  console.log("onNegativeClick");
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
  {
    label: () =>
      h(
        NButton,
        {
          ghost: true,
          type: "success",
          strong: true,
          size: "medium",
          icon: renderIcon(accountIco),
          onClick: () => auth(),
        },
        { default: () => "LogIn" }
      ),
    show: !isAuthorized.value,
    key: "auth",
  },
];
</script>
