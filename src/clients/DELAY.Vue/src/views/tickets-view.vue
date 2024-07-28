<template>
  <div class="d-flex" size="large">
    <div class="row me-5 flex-container">
      <div class="mb-4" style="display: contents">
        <div
          class="mb-3 card-border"
          style="width: max-content; height: fit-content"
        >
          <n-button type="success" @click="addBoard">
            <template #icon>
              <n-icon><plus-ico /></n-icon> </template
          ></n-button>
          <n-divider vertical style="height: 2em" />
          <n-button type="error" @click="deleteBoard">
            <template #icon>
              <n-icon><minus-ico /></n-icon> </template
          ></n-button>
        </div>
      </div>
      <div class="card-border mb-3 flex-stretch">
        <n-data-table
          :style="{
            height: '100%',
          }"
          style="min-width: 350px"
          :bordered="true"
          :single-line="false"
          :columns="boardsColumns"
          :data="boardsData"
          :row-key="rowKey"
          :pagination="pagination"
          :remote="true"
          @update:checked-row-keys="handleCheck"
        />
      </div>
    </div>
    <div class="row flex-container">
      <div class="mb-4" style="display: contents">
        <div class="card-border mb-3" style="width: max-content">
          <n-button type="success" @click="addBoard">
            <template #icon>
              <n-icon><plus-ico /></n-icon> </template
          ></n-button>
          <n-divider vertical style="height: 2em" />
          <n-button type="error" @click="deleteBoard">
            <template #icon>
              <n-icon><minus-ico /></n-icon> </template
          ></n-button>
        </div>
      </div>
      <div class="d-flex p-0 flex-stretch">
        <div class="card-border mb-3 me-4 flex-card-content">
          <div class="flex-stretch">
            <n-data-table
              :style="{
                height: '100%',
              }"
              style="min-width: 400px"
              :bordered="true"
              :single-line="false"
              :columns="boardsColumns"
              :data="boardsData"
              :row-key="rowKey"
              :pagination="pagination"
              :remote="true"
              @update:checked-row-keys="handleCheck"
            />
          </div>
          <n-button type="success" class="mt-3" ghost @click="addTicket">
            <template #icon>
              <n-icon><plus-ico /></n-icon>
            </template>
          </n-button>
        </div>
        <div class="card-border mb-3 me-4 flex-card-content">
          <div class="flex-stretch">
            <n-data-table
              :style="{
                height: '100%',
              }"
              style="min-width: 400px"
              :bordered="true"
              :single-line="false"
              :columns="boardsColumns"
              :data="boardsData"
              :row-key="rowKey"
              :pagination="pagination"
              :remote="true"
              @update:checked-row-keys="handleCheck"
            />
          </div>
          <n-button type="success" class="mt-3" ghost @click="addTicket">
            <template #icon>
              <n-icon><plus-ico /></n-icon>
            </template>
          </n-button>
        </div>
        <div class="card-border mb-3 me-4 flex-card-content">
          <div class="flex-stretch">
            <n-data-table
              :style="{
                height: '100%',
              }"
              style="min-width: 400px"
              :bordered="true"
              :single-line="false"
              :columns="boardsColumns"
              :data="boardsData"
              :row-key="rowKey"
              :pagination="pagination"
              :remote="true"
              @update:checked-row-keys="handleCheck"
            />
          </div>
          <n-button type="success" class="mt-3" ghost @click="addTicket">
            <template #icon>
              <n-icon><plus-ico /></n-icon>
            </template>
          </n-button>
        </div>
        <div class="card-border mb-3 me-4 flex-card-content">
          <div class="flex-stretch">
            <n-data-table
              :style="{
                height: '100%',
              }"
              style="min-width: 400px"
              :bordered="true"
              :single-line="false"
              :columns="boardsColumns"
              :data="boardsData"
              :row-key="rowKey"
              :pagination="pagination"
              :remote="true"
              @update:checked-row-keys="handleCheck"
            />
          </div>
          <n-button type="success" class="mt-3" ghost @click="addTicket">
            <template #icon>
              <n-icon><plus-ico /></n-icon>
            </template>
          </n-button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, h, onMounted } from "vue";
import { sendRequest } from "@/utils/request-utils";
import { NDataTable, NButton, NIcon, NDivider } from "naive-ui";
import type {
  RowData,
  TableColumn,
} from "naive-ui/es/data-table/src/interface";
import { Plus as plusIco, Minus as minusIco } from "@vicons/tabler";

const boardsData = ref([]);
const ticketsListsData = ref([]);
const ticketsData = ref([]);

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

const boardsColumns: TableColumn[] = [
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
          onClick: () => boardInfo(row),
        },
        { default: () => "Info" }
      );
    },
  },
];
const ticketsColumns: TableColumn[] = [
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
          onClick: () => ticketInfo(row),
        },
        { default: () => "Info" }
      );
    },
  },
];
const ticketsListsColumns: TableColumn[] = [
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
          onClick: () => ticketsListInfo(row),
        },
        { default: () => "Info" }
      );
    },
  },
];

function addBoard(row: any) {}

function addTicketsList(row: any) {}

function addTicket(row: any) {}

function boardInfo(row: any) {}

function ticketsListInfo(row: any) {}

function ticketInfo(row: any) {}

function deleteBoard(row: any) {}

function deleteTicketsList(row: any) {}

function deleteTicket(row: any) {}
</script>
