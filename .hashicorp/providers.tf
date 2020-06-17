provider "azurerm" {
  version = "~> 2.11.0"

  features {
    virtual_machine {
      delete_os_disk_on_deletion = true
    }

    virtual_machine_scale_set {
      roll_instances_when_required = true
    }
  }
}

provider "cloudflare" {
  version = "~> 2.7.0"
}
