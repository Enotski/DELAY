<template>
  <div class="d-flex" size="large">
    <div class="row me-5">
      <div class="mb-4" style="display: contents">
        <n-card class="box-shadow mb-3 shadow" style="width: max-content">
          <n-button type="success" ghost @click="addBoard"> Add </n-button>
          <n-divider vertical style="height: 2em" />
          <n-button type="error" ghost @click="deleteBoard"> Delete </n-button>
        </n-card>
      </div>
      <n-card
        class="box-shadow shadow p-0"
        :segmented="{
          content: true,
          footer: 'soft',
        }"
      >
        <n-data-table
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
      </n-card>
    </div>
    <div class="row">
      <div class="mb-4" style="display: contents">
        <n-card class="box-shadow mb-3 shadow" style="width: max-content">
          <n-button type="success" ghost @click="addBoard"> Add </n-button>
          <n-divider vertical style="height: 2em" />
          <n-button type="error" ghost @click="deleteBoard"> Delete </n-button>
        </n-card>
      </div>
      <div class="d-flex p-0">
        <n-card class="box-shadow shadow me-4">
          <n-button
            type="success"
            class="btn-add-ticket"
            ghost
            @click="addTicket"
          >
            Add
          </n-button>
          <n-data-table
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
        </n-card>
        <n-card class="box-shadow shadow me-4">
          <n-button
            type="success"
            class="btn-add-ticket"
            ghost
            @click="addTicket"
          >
            Add
          </n-button>
          <n-data-table
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
        </n-card>
        <n-card class="box-shadow shadow me-4">
          <n-button
            type="success"
            class="btn-add-ticket"
            ghost
            @click="addTicket"
          >
            Add
          </n-button>
          <n-data-table
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
        </n-card>
        <n-card class="box-shadow shadow me-4">
          <n-button
            type="success"
            class="btn-add-ticket"
            ghost
            @click="addTicket"
          >
            Add
          </n-button>
          <n-data-table
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
        </n-card>
        <n-card class="box-shadow shadow me-4">
          <n-button
            type="success"
            class="btn-add-ticket"
            ghost
            @click="addTicket"
          >
            Add
          </n-button>
          <n-data-table
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
        </n-card>
        <n-card class="box-shadow shadow me-4">
          <n-button
            type="success"
            class="btn-add-ticket"
            ghost
            @click="addTicket"
          >
            Add
          </n-button>
          <n-data-table
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
        </n-card>
        <n-card class="box-shadow shadow me-4">
          <n-button
            type="success"
            class="btn-add-ticket"
            ghost
            @click="addTicket"
          >
            Add
          </n-button>
          <n-data-table
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
        </n-card>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, h, onMounted } from "vue";
import { sendRequest } from "@/utils/request-utils";
import { NDataTable, NButton, NFlex, NCard, NDivider, NLayout } from "naive-ui";
import type {
  RowData,
  TableColumn,
} from "naive-ui/es/data-table/src/interface";

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
          onClick: () => borderInfo(row),
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
