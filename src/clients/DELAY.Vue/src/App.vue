<template>
  <div class="menu">
    <n-menu
      :options="menuOptions"
      default-value="/"
      mode="horizontal"
      responsive
    />
  </div>
  <n-divider title-placement="right">DELAY</n-divider>
  <div class="content"></div>
  <RouterView />
</template>

<script setup lang="ts">
import type { Component } from "vue";
import { h } from "vue";
import { RouterLink, RouterView } from "vue-router";
import { NDivider, NMenu, NIcon } from "naive-ui";
import type { MenuOption } from "naive-ui";
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
    key: "/",
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
    key: "/account",
    icon: renderIcon(accountIco),
  },
  {
    label: () =>
      h(
        RouterLink,
        {
          to: "/boards",
        },
        { default: () => "Boards" }
      ),
    key: "/boards",
    icon: renderIcon(boardIco),
  },
  {
    label: () =>
      h(
        RouterLink,
        {
          to: "/rooms",
        },
        { default: () => "Chats" }
      ),
    key: "/rooms",
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
    key: "/users",
    icon: renderIcon(usersIco),
  },
];
</script>

<style scoped>
.menu {
  width: 100%;
}
.content {
  width: 100%;
}

header {
  line-height: 1.5;
  max-height: 100vh;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

nav {
  width: 100%;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}

@media (min-width: 1024px) {
  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }

  nav {
    text-align: left;
    margin-left: -1rem;
    font-size: 1rem;

    padding: 1rem 0;
    margin-top: 1rem;
  }
}
</style>
