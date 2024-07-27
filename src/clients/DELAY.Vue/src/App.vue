<template>
  <nav
    class="navbar d-flex fixed-top bg-light justify-content-between box-shadow mb-3 shadow p-3 py-2"
  >
    <n-menu
      :options="menuOptions"
      :value="currentRouteName"
      mode="horizontal"
      responsive
    />
  </nav>
  <div class="content-container">
    <RouterView />
  </div>
</template>

<script setup lang="ts">
import type { Component } from "vue";
import { computed, h } from "vue";
import { RouterLink, RouterView } from "vue-router";
import { NMenu, NIcon } from "naive-ui";
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
const currentRouteName = computed(() => router.currentRoute.value.name);

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
          to: "/account",
        },
        { default: () => "Account" }
      ),
    key: "account",
    icon: renderIcon(accountIco),
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
    key: "users",
    icon: renderIcon(usersIco),
  },
];
</script>
