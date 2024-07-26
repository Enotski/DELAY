<template>
  <n-flex justify="space-around" size="large">
    <div>
      <n-button type="success" ghost @click="addRoom"> Add </n-button>
      <n-button type="error" ghost @click="deleteRoom"> Delete </n-button>
      <div>Chat rooms</div>
      <n-data-table
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
    <div>
      <div>Chat</div>
      <n-infinite-scroll> </n-infinite-scroll>
    </div>
  </n-flex>
</template>

<script setup lang="ts">
import { ref, h, onMounted } from "vue";
import { sendRequest } from "@/utils/request-utils";
import { NDataTable, NButton, NFlex, NInfiniteScroll } from "naive-ui";
import type {
  RowData,
  TableColumn,
} from "naive-ui/es/data-table/src/interface";

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
</script>
