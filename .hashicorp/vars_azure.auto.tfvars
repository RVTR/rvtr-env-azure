azure_kubernetes_cluster = {
  api_server_authorized_ip_ranges = []
  kubernetes_version              = ""
  dns_prefix                      = ""
  name                            = ""
  node_resource_group             = ""

  default_node_pool = {
    max_pods        = 0
    name            = ""
    node_count      = 0
    os_disk_size_gb = 0
    vm_size         = ""
  }

  linux_profile = {
    admin_username = ""

    ssh_key = {
      key_data = ""
    }
  }

  tags = {
    environment = ""
  }
}

azure_resource_group = {
  location = ""
  name     = ""

  tags = {
    environment = ""
  }
}
