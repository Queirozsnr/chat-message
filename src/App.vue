<template>
  <q-layout class="background" view="lHh Lpr lFf">
    <q-page-container class="page-container">
      <q-input
        outlined
        placeholder="Informe seu Nome"
        v-model="message.name"
        class="q-mt-sm q-pa-sm"
        dense
        >
      </q-input>
      
      <ChatComponent :messages="messages" :userActual="message.name" />

      <q-input class="q-mt-sm q-pa-sm" @keyup.enter="send" 
        placeholder="Digite uma mensagem" v-model="message.body" dense>
        <template v-slot:append>
          <q-icon size="sm" name="search" />
        </template>
      </q-input>
    </q-page-container>
  </q-layout>
</template>

<script>
import { ref, reactive, onMounted } from 'vue';
import ChatComponent from './components/ChatComponent.vue';
import Hub from "./Hub";

export default {
  name: 'LayoutDefault',

  data() {
    return {
      title: 'Chat de Mensagem',
    }
  },
  created() {
    document.title = this.title;
  },

  components: {
    ChatComponent
  },

  setup() {
    let messages = ref([]);
    let message = reactive({
      name: "",
      body: ""
    });
    let _hub = new Hub();

    function send() {
      if (message.body == "") return;
      _hub.connection.invoke("SendMessage", message);
      message.body = "";
    }

    function playNotificationSound() {
      const audio = new Audio(require('@/assets/notification.mp3'));
      audio.play();
    }

    function handleVisibilityChange() {
      if (document.hidden && messages.value.some(msg => !msg.read && msg.name !== message.name)) {
        playNotificationSound();
        messages.value.forEach(msg => msg.read = true);
      }
    }
    onMounted(() => {
      _hub.connection.start().then(() => {
        console.log("Conectado");
        _hub.connection.on("ReceivedMessage", (msg) => {
          messages.value.push({ ...msg, read: false });
          handleVisibilityChange();
        });

        document.addEventListener("visibilitychange", handleVisibilityChange);
      }).catch(e => console.log("Connection failed", e));
    });

    return {
      send,
      messages,
      message
    };
  }
};
</script>

<style>
.background {
  background-image: url('assets/background.jpg');
  background-repeat: repeat;
}

.q-field__control.relative-position.row.no-wrap {
  background-color: white;
}

.page-container {
  display: flex;
  flex-direction: column;
  height: 100vh;
  justify-content: space-between;
}

.q-field--float {
  width: 100%;
}
</style>