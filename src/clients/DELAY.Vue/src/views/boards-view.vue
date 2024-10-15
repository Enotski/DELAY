<template>
  <n-modal
    v-model:show="showBoardModal"
    class="w-100"
    preset="dialog"
    title="Edit board"
    positive-text="Confirm"
    negative-text="Cancel"
    @positive-click="onSaveBoardModal"
    @negative-click="onCloseBoardModal"
  >
    <n-form ref="boardFormRef" inline :model="boardFormValue" class="row">
      <n-form-item label="Name">
        <n-input v-model:value="boardFormValue.name" placeholder="Input Name" />
      </n-form-item>
      <n-form-item label="Description">
        <n-input
          v-model:value="boardFormValue.description"
          placeholder="Input description"
        />
      </n-form-item>
      <n-form-item>
        <n-switch
          v-model:value="boardFormValue.isPublic"
          :rail-style="railStyle"
        >
          <template #checked> Public </template>
          <template #unchecked> Private </template>
        </n-switch>
      </n-form-item>
      <div class="d-flex">
        <n-form-item label="Users">
          <n-data-table
            :style="{
              height: '100%',
            }"
            :max-height="630"
            style="min-width: 350px"
            :bordered="true"
            :single-line="false"
            :columns="boardUsersColumns"
            :data="boardUsersData"
            :row-key="rowBoardUserKey"
            :pagination="pagination"
            :remote="true"
            @update:checked-row-keys="handleCheck"
          />
        </n-form-item>
        <n-form-item label="All Users">
          <n-data-table
            :style="{
              height: '100%',
            }"
            :max-height="630"
            style="min-width: 350px"
            :bordered="true"
            :single-line="false"
            :columns="allUsersColumns"
            :data="allUsersData"
            :row-key="rowKey"
            :pagination="pagination"
            :remote="true"
            @update:checked-row-keys="handleCheck"
          />
        </n-form-item>
      </div>
    </n-form>
  </n-modal>
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
  <div class="d-flex" size="large">
    <div
      class="card-border mb-3 me-5 flex-card-content"
      style="width: inherit; height: max-content"
    >
      <n-data-table
        :style="{
          height: '100%',
        }"
        style="min-width: 400px"
        :max-height="660"
        :bordered="true"
        :single-line="false"
        :columns="boardsColumns"
        :data="boardsData"
        :row-key="rowKey"
        :pagination="pagination"
        :remote="true"
        :checked-row-keys="checkedBoardsKeys"
        @update:checked-row-keys="handleCheck"
      />
      <n-button type="success" class="mt-3" ghost @click="addBoard">
        <template #icon>
          <n-icon><plus-ico /></n-icon>
        </template>
      </n-button>
    </div>
    <div class="row flex-container" style="width: inherit; height: max-content">
      <div class="mb-4" style="display: contents">
        <div class="card-border mb-3" style="width: max-content">
          <n-button type="success" @click="addTicketsList">
            <template #icon>
              <n-icon><plus-ico /></n-icon> </template
          ></n-button>
        </div>
      </div>
      <div class="d-flex p-0 flex-stretch">
        <div v-for="(item, index) in ticketListsData" :key="index">
          <div
            class="card-border mb-3 me-4 flex-card-content"
            style="width: inherit; height: max-content"
          >
            <n-data-table
              :style="{
                height: '100%',
              }"
              style="min-width: 400px"
              :max-height="550"
              :bordered="true"
              :single-line="false"
              :columns="ticketsColumns"
              :data="item.tickets"
              :row-key="rowKey"
              :pagination="pagination"
              :remote="true"
            />
            <n-button type="success" class="mt-3" ghost @click="addTicket">
              <template #icon>
                <n-icon><plus-ico /></n-icon>
              </template>
            </n-button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, h, onMounted, type CSSProperties } from "vue";
import { RequestUtils } from "@/utils";
import {
  NDataTable,
  NButton,
  NIcon,
  NSwitch,
  NInput,
  NModal,
  NForm,
  NFormItem,
  NSelect,
} from "naive-ui";
import type {
  RowData,
  TableColumn,
} from "naive-ui/es/data-table/src/interface";
import { Add as plusIco, Remove as minusIco } from "@vicons/ionicons5";
import {
  type ITicketDto,
  type IBoardDto,
  type IBaseDto,
  type IBoardUserDto,
  BoardRoleType,
} from "@/interfaces";
import type { ITicketsListDto } from "@/interfaces/api/contracts/board/tickets-list-dto";
import { options } from "node_modules/axios/index.cjs";

const boardsData = ref<IBoardDto[]>([]);
const ticketListsData = ref<ITicketsListDto[]>([]);
const boardFormValue = ref<IBoardDto>({
  id: "",
  name: "",
  description: "",
  isPublic: true,
  users: [],
});

const railStyle = ({
  focused,
  checked,
}: {
  focused: boolean;
  checked: boolean;
}) => {
  const style: CSSProperties = {};
  if (checked) {
    style.background = "#f5770a";
    if (focused) {
      style.boxShadow = "0 0 0 2px #f5770a";
    }
  } else {
    style.background = "#2080f0";
    if (focused) {
      style.boxShadow = "0 0 0 2px #2080f040";
    }
  }
  return style;
};

const rowKey = (row: IBaseDto) => row.id;
const rowBoardUserKey = (row: IBoardUserDto) => row.user.id;
const checkedBoardsKeys = ref<string[]>([]);

const handleCheck = (rowKeys: any) => {
  checkedBoardsKeys.value = rowKeys;
};

const pagination = {
  pageSize: 10,
};

onMounted(async () => {
  await RequestUtils.default
    .sendRequest("boards/by-user", "GET")
    .then(async (response: IBoardDto[]) => {
      if (response != null) {
        boardsData.value = response;

        checkedBoardsKeys.value.push(boardsData.value[0].id);
        if (boardsData.value.length > 0) {
          await RequestUtils.default
            .sendRequest("tickets-lists/by-board", "GET", {
              boardId: boardsData.value[0].id,
            })
            .then((res: ITicketsListDto[]) => {
              if (res != null) {
                ticketListsData.value = res;
              }
            })
            .finally(() => {
              console.log("get tickets list by board");
            });
        }
      }
    })
    .finally(() => {
      console.log("get boards by user");
    });
});

const boardsColumns: TableColumn<IBoardDto>[] = [
  {
    title: "Boards",
    align: "center",
    key: "name",
    render(row, index) {
      return h(NInput, {
        value: row.name,
        onUpdateValue(v) {
          boardsData.value[index].name = v;
          console.log(boardsData.value[index].name);
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
          strong: true,
          type: "info",
          size: "small",
          onClick: () => boardInfo(row),
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
          type: "error",
          strong: true,
          size: "small",
          onClick: () => deleteBoard(row),
        },
        { default: () => "Delete" }
      );
    },
  },
];

const ticketsColumns: TableColumn<ITicketDto>[] = [
  {
    title: "Tickets",
    align: "center",
    key: "name",
    render(row) {
      return h(NInput, {
        value: row.name,
      });
    },
  },
  {
    title(column: any) {
      return h(
        NButton,
        {
          type: "error",
          strong: true,
          size: "small",
          onClick: () => deleteTicketsList(column),
        },
        { default: () => "Delete list" }
      );
    },
    align: "center",
    key: "editButtons",
    children: [
      {
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
              onClick: () => ticketInfo(row),
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
              onClick: () => deleteTicket(row),
            },
            { default: () => "Delete" }
          );
        },
      },
    ],
  },
];

const userBoardRoleOpetions = [
  {
    label: "User",
    value: "1",
  },
  {
    label: "Moder",
    value: "2",
  },
  {
    label: "Admin",
    value: "3",
  },
];

const boardUsersColumns: TableColumn<IBoardUserDto>[] = [
  {
    type: "selection",
  },
  {
    title: "Name",
    key: "user.name",
  },
  {
    title: "Role",
    key: "userRole",
    render(row, index) {
      return h(NSelect, {
        options: userBoardRoleOpetions,
        value: row.userRole,
        onUpdateValue(v) {
          boardUsersData.value[index].userRole = v;
        },
      });
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
          onClick: () => deleteUserFromBoard(row),
        },
        { default: () => "Delete" }
      );
    },
  },
];
const allUsersColumns: TableColumn[] = [
  {
    type: "selection",
  },
  {
    title: "Name",
    key: "name",
  },
  {
    title: "",
    key: "success",
    width: 68,
    render(row) {
      return h(
        NButton,
        {
          ghost: true,
          type: "success",
          strong: true,
          size: "small",
          onClick: () => addUserToBoard(row),
        },
        { default: () => "Add" }
      );
    },
  },
];
const boardUsersData = ref<IBoardUserDto[]>([
  {
    user: {
      id: "110",
      name: "John Brown",
    },
    userRole: "1",
  },
  {
    user: {
      id: "111",
      name: "Jim Green111",
    },
    userRole: "2",
  },
  {
    user: {
      id: "112",
      name: "Jim Green11213",
    },
    userRole: "1",
  },
  {
    user: {
      id: "113",
      name: "Jim Green3333",
    },
    userRole: "2",
  },
]);

const allUsersData = ref([
  {
    id: 110,
    name: "John Brown",
  },
  {
    id: 111,
    name: "Jim Green",
  },
  {
    id: 112,
    name: "Joe Black",
  },
  {
    id: 111,
    name: "John Brown",
  },
  {
    id: 212,
    name: "Jim Green",
  },
  {
    id: 313,
    name: "Joe Black",
  },
  {
    id: 112,
    name: "John Brown",
  },
  {
    id: 131,
    name: "Jim Green",
  },
  {
    id: 0,
    name: "John Brown",
  },
  {
    id: 1,
    name: "Jim Green",
  },
  {
    id: 2,
    name: "Joe Black",
  },
  {
    id: 11,
    name: "John Brown",
  },
  {
    id: 22,
    name: "Jim Green",
  },
  {
    id: 33,
    name: "Joe Black",
  },
  {
    id: 12,
    name: "John Brown",
  },
  {
    id: 31,
    name: "Jim Green",
  },
  {
    id: 21,
    name: "Joe Black",
  },
  {
    id: 10,
    name: "John Brown",
  },
  {
    id: 41,
    name: "Jim Green",
  },
  {
    id: 92,
    name: "Joe Black",
  },
]);
const showBoardModal = ref(false);
const showModal = ref(false);

async function addBoard(row: any) {
  // await RequestUtils.sendRequest("boards", "GET").then(async () => {
  //   console.log("good");
  // });
  showBoardModal.value = true;
  console.log("addBoard");
}

async function addUserToBoard(row: any) {
  console.log("addUserToBoard");
}
async function deleteUserFromBoard(row: any) {
  console.log("addUserToBoard");
}

function onSaveBoardModal() {
  console.log("addTicketsList");
}
function onCloseBoardModal() {
  console.log("addTicketsList");
}

function addTicketsList(row: any) {
  console.log("addTicketsList");
}

function addTicket(row: any) {
  console.log("addTicket");
}

function boardInfo(row: any) {
  showBoardModal.value = true;
  console.log("boardInfo");
}

function ticketInfo(row: any) {
  showModal.value = true;
  console.log("ticketInfo");
}

function onPositiveClick() {
  console.log("onPositiveClick");
}
function onNegativeClick() {
  showModal.value = false;
  console.log("onNegativeClick");
}

function deleteBoard(row: any) {
  showModal.value = true;
  console.log("deleteBoard");
}

function deleteTicketsList(row: any) {
  showModal.value = true;
  console.log("deleteTicketsList");
}

function deleteTicket(row: any) {
  showModal.value = true;
  console.log("deleteTicket");
}
</script>
