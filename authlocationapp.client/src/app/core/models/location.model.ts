export interface Location {
  id: number;
  name: string;
}

export interface Country extends Location {}

export interface Province extends Location {}
