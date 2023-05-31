<template>
  <h1>Full Stack app</h1>
  <form @submit.prevent="createDriver">
    <input type="text" v-model="name">
    <input type="text" v-model="number">
    <input type="text" v-model="team">
    <button type="submit">Create</button>
  </form>
  <div v-for="(d, i) in fetchedData" :key="i">
    <ul>
      <li>{{d.name}}</li>
      <li>{{d.number}}</li>
      <li>{{d.team}}</li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  data() {
    return {
      fetchedData: [],
      name: '',
      number: 0,
      team: ''
    }
  },
  mounted() {
    axios.get('http://localhost:5000/api/Drivers')
        .then(res => this.fetchedData = res.data)
  },
  methods: {
    createDriver() {
      const newDriver = {
        name: this.name,
        number: this.number,
        team: this.team
      }
      axios.post('http://localhost:5000/api/Drivers', newDriver)
          .then(res => this.fetchedData.push(res.data))
      this.name = ''
      this.number = 0
      this.team = ''
    }
  }
}
</script>

<style>
</style>
