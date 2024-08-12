<template>
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
  <div class="row flex-container">
    <div class="mb-4" style="display: contents">
      <div class="card-border mb-3" style="width: max-content">
        <n-button type="success" @click="addUser">
          <template #icon>
            <n-icon><plus-ico /></n-icon>
          </template>
        </n-button>
      </div>
    </div>
    <div class="card-border flex-stretch">
      <n-data-table
        :max-height="600"
        :bordered="true"
        :single-line="false"
        :columns="usersColumns"
        :data="tableData"
        :row-key="id"
        :pagination="pagination"
        :remote="true"
        @update:checked-row-keys="handleCheck"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, h, onMounted } from "vue";
import { sendRequest } from "@/utils/request-utils";
import { NDataTable, NButton, NInput, NIcon, NDivider, NModal } from "naive-ui";
import type {
  RowData,
  TableColumn,
} from "naive-ui/es/data-table/src/interface";

import { Plus as plusIco, Minus as minusIco } from "@vicons/tabler";

const tableData = ref();

const id = (row: any) => row.id;
const checkedRowKeysRef = ref([]);

const handleCheck = (rowKeys: any) => {
  checkedRowKeysRef.value = rowKeys;
};

const pagination = {
  pageSize: 10,
};

const showModal = ref(false);

const defaultRequestOptions = {
  searchs: [
    {
      column: "",
      value: "",
    },
  ],
  sorts: [
    {
      column: "",
      order: 0,
    },
  ],
  pagination: {
    skip: 0,
    take: 10,
  },
};

onMounted(async () => {
  await sendRequest("users/get-all", "POST", defaultRequestOptions).then(
    (value) => {
      tableData.value = value.data;
    }
  );
});

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
    render(row, index) {
      return h(NInput, {
        value: row.name,
        onUpdateValue(v) {
          tableData.value[index].name = v;
          console.log(tableData.value[index].name);
        },
      });
    },
  },
  {
    title: "",
    key: "info",
    width: 68,
    render(row) {
      return h(
        NButton,
        {
          ghost: true,
          type: "info",
          strong: true,
          size: "small",
          onClick: () => userInfo(row),
        },
        { default: () => "Info" }
      );
    },
  },
  {
    width: 80,
    key: "delete",
    render(row) {
      return h(
        NButton,
        {
          ghost: true,
          type: "error",
          strong: true,
          size: "small",
          onClick: () => deleteUser(row),
        },
        { default: () => "Delete" }
      );
    },
  },
];

function addUser(row: any) {
  tableData.value.push({
    id: tableData.value.length,
    name: "New User_" + tableData.value.length,
  });
  console.log("addUser");
}

function userInfo(row: any) {
  showModal.value = true;
  console.log("userInfo");
}

function onPositiveClick(row: any) {
  console.log("onPositiveClick");
}
function onNegativeClick(row: any) {
  showModal.value = false;
  console.log("onNegativeClick");
}

function deleteUser(row: any) {
  showModal.value = true;
  console.log("deleteUser");
}
</script>
