variable "record" {
  type = object({
    name  = string
    type  = string
    value = string
  })
}

variable "zones" {
  type = object({
    name = string
  })
}
