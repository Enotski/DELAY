<template>
  <n-flex justify="space-around" size="large">
    <div>
      <n-button type="success" ghost @click="addBoard"> Add </n-button>
      <n-button type="error" ghost @click="deleteBoard"> Delete </n-button>
      <div>Boards</div>
      <n-data-table
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
    <div>
      <n-button type="success" ghost @click="addTicketsList"> Add </n-button>
      <n-button type="error" ghost @click="deleteTicketsList">
        Delete
      </n-button>
      <div>TicketsLists</div>
      <n-data-table
        :bordered="true"
        :single-line="false"
        :columns="ticketsListsColumns"
        :data="ticketsListsData"
        :row-key="rowKey"
        :pagination="pagination"
        :remote="true"
        @update:checked-row-keys="handleCheck"
      />
    </div>
    <div>
      <n-button type="success" ghost @click="addTicket"> Add </n-button>
      <n-button type="error" ghost @click="deleteTicket"> Delete </n-button>
      <div>Tickets</div>
      <n-data-table
        :bordered="true"
        :single-line="false"
        :columns="ticketsColumns"
        :data="ticketsData"
        :row-key="rowKey"
        :pagination="pagination"
        :remote="true"
        @update:checked-row-keys="handleCheck"
      />
    </div>
  </n-flex>
</template>

<script setup lang="ts">
import { ref, h, onMounted } from "vue";
import { sendRequest } from "@/utils/request-utils";
import { NDataTable, NButton, NFlex } from "naive-ui";
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
