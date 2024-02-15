<template>
    <div ref="chatContainer" class="q-pa-md row full-height justify-center items-end scroll">
      <div style="width: 100%; max-width: 80%">
        <div ref="messagesContainer" style="width: 100%;" v-for="(message, index) in messages" :key="index">
          <q-chat-message 
            :text="[message.body]" 
            sent
            :name="message.name"
            :stamp="message.date"
            v-if="message.name == userActual"
          />
          <q-chat-message v-else
            :text="[message.body]" 
            :name="message.name"
            :stamp="message.date"
          />
        </div>
      </div>
    </div>
  </template>
    
  <script>
  export default {
    name: 'ChatComponent',
    props: {
      userActual: String,
      messages: Array,
    },
    mounted() {
      this.scrollToBottom();
    },
    watch: {
      messages: {
        handler() {
          this.$nextTick(() => {
            this.scrollToBottom();
          });
        },
        deep: true,
      },
    },
    methods: {
      scrollToBottom() {
        const messagesContainer = this.$refs.messagesContainer;
        if (messagesContainer && messagesContainer.length > 0) {
          const lastMessage = messagesContainer[messagesContainer.length - 1];
          lastMessage.scrollIntoView({ behavior: "smooth", block: "end" });
        }
      },
    },
  };
  </script>
  