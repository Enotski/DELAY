<template>
  <div class="d-flex justify-content-between" size="large">
    <div class="col me-4 flex-container">
      <div class="mb-4" style="display: contents">
        <div class="mb-3 card-border" style="width: max-content">
          <n-button type="primary" @click="addRoom">
            <template #icon>
              <n-icon><plus-ico /></n-icon>
            </template>
          </n-button>
          <n-divider vertical style="height: 2em" />
          <n-button type="error" @click="deleteRoom">
            <template #icon>
              <n-icon><minus-ico /></n-icon>
            </template>
          </n-button>
        </div>
      </div>
      <div class="card-border flex-stretch">
        <n-data-table
          :style="{
            height: '100%',
          }"
          style="min-width: 350px"
          :bordered="true"
          :single-line="false"
          :columns="roomsColumns"
          :data="roomsData"
          :row-key="rowKey"
          :pagination="pagination"
          :remote="true"
          @update:checked-row-keys="handleCheck"
        />
      </div>
    </div>
    <div class="col me-4 flex-container">
      <div class="d-flex flex-column card-border mb-3 flex-stretch">
        <div class="text-center"><h5>Chat</h5></div>
        <div style="height: 70vh">
          <n-infinite-scroll :distance="10" @load="handleLoad">
            <div
              v-for="(item, index) in items"
              :key="item.key"
              class="message"
              :class="{ reverse: index % 5 === 0 }"
            >
              <img class="avatar" :src="item.avatar" alt="" />
              <span> {{ item.message }} {{ index % 5 === 0 ? "?" : "" }}</span>
            </div>
            <div v-if="loading" class="text">Loading...</div>
            <div v-if="noMore" class="text">No More 🤪</div>
          </n-infinite-scroll>
        </div>
      </div>
      <div class="mb-4" style="display: contents">
        <div class="card-border" style="width: inherit; height: max-content">
          <n-input-group>
            <n-input />
            <n-button type="primary" @click="sendMesage">
              <template #icon>
                <n-icon><send-ico /></n-icon>
              </template>
            </n-button>
          </n-input-group>
        </div>
      </div>
    </div>
    <div class="col card-border flex-container">
      <div class="mb-4" style="width: inherit; height: max-content">
        <n-input-group>
          <n-select
            v-model:value="selectedUsersValues"
            multiple
            filterable
            placeholder="Search users"
            :options="usersOptions"
            :loading="loading"
            clearable
            remote
            :clear-filter-after-select="false"
            @search="handleUsersSearch"
          />
          <n-button type="primary" @click="addUserToRoom">
            <template #icon>
              <n-icon><plus-ico /></n-icon>
            </template>
          </n-button>
        </n-input-group>
      </div>
      <div class="flex-stretch">
        <n-data-table
          :style="{
            height: '100%',
          }"
          :bordered="true"
          :single-line="false"
          :columns="roomsColumns"
          :data="roomsData"
          :row-key="rowKey"
          :pagination="pagination"
          :remote="true"
          @update:checked-row-keys="handleCheck"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, h, onMounted, computed } from "vue";
import { sendRequest } from "@/utils/request-utils";
import {
  NDataTable,
  NButton,
  NInput,
  NInputGroup,
  NIcon,
  NInfiniteScroll,
  NDivider,
  NSelect,
} from "naive-ui";
import type {
  RowData,
  TableColumn,
} from "naive-ui/es/data-table/src/interface";

import {
  Plus as plusIco,
  Minus as minusIco,
  Send as sendIco,
} from "@vicons/tabler";

const roomsData = ref([]);

const rowKey = (row: any) => row.id;
const checkedRowKeysRef = ref([]);

const handleCheck = (rowKeys: any) => {
  checkedRowKeysRef.value = rowKeys;
};

const pagination = {
  pageSize: 10,
};

const defaultRequestOptions = {
  searchOptions: [
    {
      column: "",
      value: "",
    },
  ],
  sortOptions: [
    {
      column: "",
      order: 0,
    },
  ],
  paginatedOption: {
    skip: 0,
    take: 0,
  },
};
let usersOptions: any[];
let selectedUsersValues: any[];
onMounted(async () => {});

// const handleAddClick = async () => {
//   await sendRequest("users", "POST", formValue.value.form).then(() => {
//     sendRequest("users/search", "POST", defaultRequestOptions).then((value) => {
//       data.value = value.records;
//     });
//     formValue.value.form = {
//       name: "",
//       login: "",
//       password: "",
//     };
//   });
// };

// const handleRemoveClick = async () => {
//   await sendRequest("users", "DELETE", checkedRowKeysRef.value).then(
//     async () => {
//       await sendRequest("users/search", "POST", defaultRequestOptions).then(
//         (value) => {
//           data.value = value.records;
//         }
//       );
//     }
//   );
// };

const roomsColumns: TableColumn[] = [
  {
    type: "selection",
  },
  {
    title: "Name",
    key: "name",
  },
  {
    title: "",
    key: "info",
    render(row) {
      return h(
        NButton,
        {
          strong: true,
          tertiary: true,
          size: "small",
          onClick: () => roomInfo(row),
        },
        { default: () => "Info" }
      );
    },
  },
];

function addRoom(row: any) {}

function roomInfo(row: any) {}

function deleteRoom(row: any) {}
function sendMesage(row: any) {}
function addUserToRoom(row: any) {}
function handleUsersSearch(arg: string) {}

const loading = ref(false);

const avatars = [
  "https://07akioni.oss-cn-beijing.aliyuncs.com/07akioni.jpeg",
  "https://avatars.githubusercontent.com/u/20943608?s=60&v=4",
  "https://avatars.githubusercontent.com/u/46394163?s=60&v=4",
  "https://avatars.githubusercontent.com/u/39197136?s=60&v=4",
  "https://avatars.githubusercontent.com/u/19239641?s=60&v=4",
];

const messages = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];

const mock = (i: number) => ({
  key: `${i}`,
  value: i,
  avatar: avatars[i % avatars.length],
  message: messages[Math.floor(Math.random() * messages.length)],
});

const items = ref(Array.from({ length: 10 }, (_, i) => mock(i)));
const noMore = computed(() => items.value.length > 16);

const handleLoad = async () => {
  if (loading.value || noMore.value) return;
  loading.value = true;
  await new Promise((resolve) => setTimeout(resolve, 1000));
  items.value.push(...[mock(items.value.length), mock(items.value.length + 1)]);
  loading.value = false;
};
</script>

<style scoped>
.message {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
  padding: 10px;
}

.message:last-child {
  margin-bottom: 0;
}

.reverse {
  flex-direction: row-reverse;
}

.text {
  text-align: center;
}

.reverse .avatar {
  margin-left: 10px;
}

.avatar {
  width: 28px;
  height: 28px;
  border-radius: 50%;
  margin-right: 10px;
}
</style>
