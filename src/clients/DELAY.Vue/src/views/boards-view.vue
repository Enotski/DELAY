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
import { ref, h, onMounted } from "vue";
import { RequestUtils } from "@/utils";
import { NDataTable, NButton, NIcon, NDivider, NInput, NModal } from "naive-ui";
import type {
  RowData,
  TableColumn,
} from "naive-ui/es/data-table/src/interface";
import { Add as plusIco, Remove as minusIco } from "@vicons/ionicons5";
import type { ITicketDto, IBoardDto, IBaseDto } from "@/interfaces";
import type { ITicketsListDto } from "@/interfaces/api/contracts/board/tickets-list-dto";

const boardsData = ref<IBoardDto[]>([]);
const ticketListsData = ref<ITicketsListDto[]>([]);

const rowKey = (row: IBaseDto) => row.id;
const checkedBoardsKeys = ref<string[]>([]);

const handleCheck = (rowKeys: any) => {
  checkedBoardsKeys.value = rowKeys;
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

const showModal = ref(false);

async function addBoard(row: any) {
  // await RequestUtils.sendRequest("boards", "GET").then(async () => {
  //   console.log("good");
  // });
  console.log("addBoard");
}

function addTicketsList(row: any) {
  console.log("addTicketsList");
}

function addTicket(row: any) {
  console.log("addTicket");
}

function boardInfo(row: any) {
  showModal.value = true;
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
