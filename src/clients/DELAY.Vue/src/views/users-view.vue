<template>
  <div class="row">
    <div class="mb-4" style="display: contents">
      <n-card class="box-shadow mb-3 shadow" style="width: max-content">
        <n-button type="success" ghost @click="addUser"> Add </n-button>
        <n-divider vertical style="height: 2em" />
        <n-button type="error" ghost @click="deleteUser"> Delete </n-button>
      </n-card>
    </div>
    <n-card
      class="box-shadow mb-3 shadow"
      :segmented="{
        content: true,
        footer: 'soft',
      }"
    >
      <n-data-table
        :bordered="true"
        :single-line="false"
        :columns="usersColumns"
        :data="usersData"
        :row-key="rowKey"
        :pagination="pagination"
        :remote="true"
        @update:checked-row-keys="handleCheck"
      />
    </n-card>
  </div>
</template>

<script setup lang="ts">
import { ref, h, onMounted } from "vue";
import { sendRequest } from "@/utils/request-utils";
import { NDataTable, NButton, NCard, NDivider } from "naive-ui";
import type {
  RowData,
  TableColumn,
} from "naive-ui/es/data-table/src/interface";

const usersData = ref([]);

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

const usersColumns: TableColumn[] = [
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
          onClick: () => userInfo(row),
        },
        { default: () => "Info" }
      );
    },
  },
];

function addUser(row: any) {}

function userInfo(row: any) {}

function deleteUser(row: any) {}
</script>
