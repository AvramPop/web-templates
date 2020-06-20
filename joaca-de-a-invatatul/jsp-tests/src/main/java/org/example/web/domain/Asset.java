package org.example.web.domain;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class Asset {
  private int id;
  private int userId;
  private String name;
  private String description;
  private int value;
}
