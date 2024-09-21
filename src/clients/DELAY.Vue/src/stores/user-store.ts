import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useUserStore = defineStore('user', () => {
  const user = ref({
        id:"",
        name: "",
        email: "",
        phone: "",
        role: 0,
    });
   const setUser = (model: any) => {
        user.value = model;
    };
   const clearUser = () => {
      user.value = {
        name: "",
        email: "",
        phone: "",
        role: 0,
        id: ""
    }
}
    return {user, setUser, clearUser};
  })