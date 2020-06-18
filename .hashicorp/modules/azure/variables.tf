variable "kubernetes_cluster" {
  type = object({
    dns_prefix                      = string
    kubernetes_version              = string
    name                            = string

    default_node_pool = object({
      max_pods        = number
      name            = string
      node_count      = number
      vm_size         = string
    })

    linux_profile = object({
      admin_username = string

      ssh_key = object({
        key_data = string
      })
    })

    tags = object({
      environment = string
    })
  })
}

variable "resource_group" {
  type = object({
    location = string
    name     = string

    tags = object({
      environment = string
    })
  })
}
