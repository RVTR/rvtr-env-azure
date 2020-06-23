resource "azurerm_kubernetes_cluster" "rvtr" {
  dns_prefix                      = var.kubernetes_cluster.dns_prefix
  kubernetes_version              = var.kubernetes_cluster.kubernetes_version
  location                        = azurerm_resource_group.rvtr.location
  name                            = var.kubernetes_cluster.name
  node_resource_group             = var.kubernetes_cluster.node_resource_group
  private_cluster_enabled         = false
  resource_group_name             = azurerm_resource_group.rvtr.name

  addon_profile {
    kube_dashboard {
      enabled = true
    }
  }

  default_node_pool {
    enable_auto_scaling   = false
    enable_node_public_ip = false
    max_pods              = var.kubernetes_cluster.default_node_pool.max_pods
    name                  = var.kubernetes_cluster.default_node_pool.name
    node_count            = var.kubernetes_cluster.default_node_pool.node_count
    type                  = "VirtualMachineScaleSets"
    vm_size               = var.kubernetes_cluster.default_node_pool.vm_size
  }

  identity {
    type = "SystemAssigned"
  }

  linux_profile {
    admin_username = var.kubernetes_cluster.linux_profile.admin_username

    ssh_key {
      key_data = var.kubernetes_cluster.linux_profile.ssh_key.key_data
    }
  }

  role_based_access_control {
    enabled = true
  }

  tags = {
    environment = var.environment_definition.name
  }
}

resource "azurerm_public_ip" "rvtr" {
  allocation_method = "Static"
  domain_name_label = random_pet.rvtr.id
  ip_version = "IPv4"
  location = azurerm_resource_group.rvtr.location
  name = var.public_ip.name
  resource_group_name = azurerm_kubernetes_cluster.rvtr.node_resource_group
  sku = "Basic"

  tags = {
    environment = var.environment_definition.name
  }
}

resource "azurerm_resource_group" "rvtr" {
  location = var.resource_group.location
  name     = var.resource_group.name

  tags = {
    environment = var.environment_definition.name
  }
}

resource "random_pet" "rvtr" {
  length = 3
  separator = "-"
}
