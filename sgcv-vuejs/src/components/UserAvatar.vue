<template>
  <svg :width="size" :height="size" viewBox="0 0 100 100" xmlns="http://www.w3.org/2000/svg">
    <circle cx="50" cy="50" :r="50" :fill="backgroundColor" />
    <text x="50"
          y="50"
          font-family="Arial"
          :font-size="fontSize"
          font-weight="bold"
          text-anchor="middle"
          dominant-baseline="central"
          fill="#FFFFFF">
      {{ initials }}
    </text>
  </svg>
</template>

<script>
export default {
  name: 'UserAvatar',
  props: {
    name: {
      type: String,
      required: true
    },
    size: {
      type: Number,
      default: 40
    }
  },
  computed: {
    initials() {
      if (!this.name) return '?';

      return this.name
        .split(' ')
        .map(part => part.charAt(0))
        .join('')
        .toUpperCase()
        .substring(0, 2);
    },
    backgroundColor() {
      const colors = [
        '#1abc9c', '#2ecc71', '#3498db', '#9b59b6', '#34495e',
        '#16a085', '#27ae60', '#2980b9', '#8e44ad', '#2c3e50',
        '#f1c40f', '#e67e22', '#e74c3c', '#ecf0f1', '#95a5a6',
        '#f39c12', '#d35400', '#c0392b', '#bdc3c7', '#7f8c8d'
      ];

      let hash = 0;
      for (let i = 0; i < this.name.length; i++) {
        hash = this.name.charCodeAt(i) + ((hash << 5) - hash);
      }

      return colors[Math.abs(hash) % colors.length];
    },
    fontSize() {
      return 40;
    }
  }
}
</script>
