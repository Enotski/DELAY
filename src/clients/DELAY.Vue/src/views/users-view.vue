<template>
  <n-modal
    v-model:show="showConfirmRemoveModal"
    preset="dialog"
    title="Remove users"
    content="Are you sure?"
    positive-text="Confirm"
    negative-text="Cancel"
    @positive-click="onConfirmRemoveClick"
    @negative-click="onCancelRemoveClick"
  />
  <n-modal
    v-model:show="showAddModal"
    preset="dialog"
    title="Add user"
    positive-text="Confirm"
    negative-text="Cancel"
    @positive-click="onConfirmAddClick"
    @negative-click="onCancelAddClick"
  >
    <n-form
      ref="formRef"
      inline
      :label-width="80"
      :model="formValue"
      :rules="addRules"
      size="large"
      class="row"
    >
      <n-form-item label="Name" path="name">
        <n-input v-model:value="formValue.name" placeholder="Input Name" />
      </n-form-item>
      <n-form-item label="Password" path="password">
        <n-input
          v-model:value="formValue.password"
          placeholder="Input Password"
        />
      </n-form-item>
      <n-form-item label="Email" path="email">
        <n-input v-model:value="formValue.email" placeholder="Input Email" />
      </n-form-item>
      <n-form-item label="Phone" path="phoneNumber">
        <n-input
          v-model:value="formValue.phoneNumber"
          placeholder="Input Phone"
        />
      </n-form-item>
    </n-form>
  </n-modal>

  <n-modal
    v-model:show="showEditModal"
    preset="dialog"
    title="Edit user"
    positive-text="Confirm"
    negative-text="Cancel"
    @positive-click="onConfirmEditClick"
    @negative-click="onCancelEditClick"
  >
    <n-form
      ref="formRef"
      inline
      :label-width="80"
      :model="formValue"
      :rules="editRules"
      size="large"
      class="row"
    >
      <n-form-item label="Name" path="name">
        <n-input v-model:value="formValue.name" placeholder="Input Name" />
      </n-form-item>
      <n-form-item label="Email" path="email">
        <n-input v-model:value="formValue.email" placeholder="Input Email" />
      </n-form-item>
      <n-form-item label="Phone" path="phoneNumber">
        <n-input
          v-model:value="formValue.phoneNumber"
          placeholder="Input Phone"
        />
      </n-form-item>
    </n-form>
  </n-modal>
  <div class="row flex-container">
    <div class="mb-4" style="display: contents">
      <div class="card-border mb-3" style="width: max-content">
        <n-button type="success" @click="addUser">
          <template #icon>
            <n-icon><plus-ico /></n-icon>
          </template>
        </n-button>
        <n-divider vertical style="height: 2em" />
        <n-button type="error" @click="removeUsers">
          <template #icon>
            <n-icon><minus-ico /></n-icon>
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
import {
  NDataTable,
  NButton,
  NInput,
  NIcon,
  NForm,
  NFormItem,
  NModal,
  NDivider,
} from "naive-ui";
import type {
  RowData,
  TableColumn,
} from "naive-ui/es/data-table/src/interface";

import { Plus as plusIco, Minus as minusIco } from "@vicons/tabler";

const tableData = ref();

const formValue = ref({
  id: "",
  name: "",
  email: "",
  phoneNumber: "",
  password: "",
});

const addRules = {
  name: {
    required: true,
    message: "Please input your name",
    trigger: "blur",
  },
  password: {
    required: true,
    message: "Please input your password",
    trigger: "blur",
  },
};
const editRules = {
  name: {
    required: true,
    message: "Please input your name",
    trigger: "blur",
  },
};

const id = (row: any) => row.id;
const checkedRowKeysRef = ref([]);

const handleCheck = (rowKeys: any) => {
  checkedRowKeysRef.value = rowKeys;
};

const pagination = {
  pageSize: 10,
};

const showAddModal = ref(false);
const showEditModal = ref(false);
const showConfirmRemoveModal = ref(false);

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

const usersColumns: TableColumn[] = [
  {
    type: "selection",
  },
  {
    title: "Name",
    key: "name",
  },
  {
    title: "Email",
    key: "email",
  },
  {
    title: "Phone",
    key: "phoneNumber",
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

async function addUser() {
  showAddModal.value = true;
}
async function removeUsers() {
  showConfirmRemoveModal.value = true;
}

function userInfo(row: any) {
  showEditModal.value = true;

  formValue.value = row;
  formValue.value.password = "";
}

async function onConfirmAddClick() {
  await sendRequest("users", "POST", formValue.value).then(async () => {
    await sendRequest("users/get-all", "POST", defaultRequestOptions).then(
      (value) => {
        tableData.value = value.data;
      }
    );
    closeAddModal();
  });
}
function onCancelAddClick() {
  closeAddModal();
}
function closeAddModal() {
  formValue.value = {
    id: "",
    name: "",
    email: "",
    phoneNumber: "",
    password: "",
  };

  showAddModal.value = false;
}

function closeEditModal() {
  formValue.value = {
    id: "",
    name: "",
    email: "",
    phoneNumber: "",
    password: "",
  };

  showEditModal.value = false;
}

async function onConfirmEditClick() {
  await sendRequest("users", "PUT", formValue.value).then(async () => {
    await sendRequest("users/get-all", "POST", defaultRequestOptions).then(
      (value) => {
        tableData.value = value.data;
      }
    );
    closeAddModal();
  });
}
function onCancelEditClick() {
  closeEditModal();
}

async function deleteUser(row: any) {
  await sendRequest("users", "DELETE", row.id).then(async () => {
    await sendRequest("users/get-all", "POST", defaultRequestOptions).then(
      (value) => {
        tableData.value = value.data;
      }
    );
  });
}
async function onConfirmRemoveClick() {
  await sendRequest(
    "users/delete-multiple",
    "POST",
    checkedRowKeysRef.value
  ).then(async () => {
    await sendRequest("users/get-all", "POST", defaultRequestOptions).then(
      (value) => {
        tableData.value = value.data;
      }
    );
  });
  showConfirmRemoveModal.value = false;
}
function onCancelRemoveClick() {
  showConfirmRemoveModal.value = false;
}
</script>
