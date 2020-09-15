provider "azurerm" {
  features {
    virtual_machine {
      delete_os_disk_on_deletion = true
    }

    virtual_machine_scale_set {
      roll_instances_when_required = true
    }
  }
}

provider "cloudflare" {}

provider "random" {}
