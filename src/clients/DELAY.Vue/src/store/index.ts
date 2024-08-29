import Vuex from 'vuex'

const store = new Vuex.Store({
  state: {
    user: {
        id:"",
        name: "",
        email: "",
        phone: "",
        role: 0,
    }
  },
  mutations: {
    setUser (state, model: any) {
        state.user = model;
    },
    clearUser (state) {
      state.user = {
        name: "",
        email: "",
        phone: "",
        role: 0,
        id: ""
    };
    }
  }
})

export default store;