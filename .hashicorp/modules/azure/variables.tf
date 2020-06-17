variable "kubernetes_cluster" {
  type = object({
    api_server_authorized_ip_ranges = set(string)
    dns_prefix                      = string
    kubernetes_version              = string
    name                            = string
    node_resource_group             = string

    default_node_pool = object({
      max_pods        = number
      name            = string
      node_count      = number
      os_disk_size_gb = number
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
