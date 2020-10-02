variable "environment_definition" {
  type = object({
    name = string
  })
}

variable "kubernetes_cluster" {
  type = object({
    dns_prefix          = string
    kubernetes_version  = string
    name                = string
    node_resource_group = string

    default_node_pool = object({
      max_pods   = number
      name       = string
      node_count = number
      vm_size    = string
    })

    linux_profile = object({
      admin_username = string

      ssh_key = object({
        key_data = string
      })
    })
  })
}

variable "public_ip" {
  type = object({
    name = string
  })
}

variable "resource_group" {
  type = object({
    location = string
    name     = string
  })
}

variable "storage_account" {
  type = object({
    name = string
  })
}

variable "storage_container" {
  type = object({
    directories = list(string)
  })
}

variable "storage_share" {
  type = object({
    name  = string
    quota = number
  })
}
